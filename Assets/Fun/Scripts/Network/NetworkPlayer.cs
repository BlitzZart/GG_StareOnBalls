using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    private NW_Ball currentBall;

    #region unity callbacks
    void Start() {
        if (!isServer && hasAuthority) {
            Transform camTrans = Camera.main.transform;
            camTrans.rotation = Quaternion.Euler(50, 180, 0);
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y, -camTrans.position.z);
        }
        BallServer.EventGameStarted += OnGameStarted;
    }
    void OnDestroy() {
        BallServer.EventGameStarted -= OnGameStarted;
    }
    #endregion

    #region public
    public void UsePowerUp(PowerUpType type, GameObject go) {
        PowerUpFactory.ActivatePowerUp(type);

        if (go != null)
            NetworkServer.Destroy(go);
    }
    #endregion

    #region private
    private void OnGameStarted() {
        if (!isServer)
            return;
        GameStarted();
        RpcStartGame();
    }

    private void GameStarted() {
        NetworkManagerHUD hud = FindObjectOfType<NetworkManagerHUD>();
        if (hud == null)
            return;

        hud.showGUI = false;

        SoundManager.Instance.StartMusic();
    }
    #endregion

    #region network
    [Command] // player 2 looks at ball
    public void CmdHasFocus(int ballNumber, bool hasFocus) {
        NW_Ball ball = BallServer.Instace.balls[ballNumber];

        if (ball == null) {
            currentBall = null;
            return;
        }
        ball.p2HasFocus = hasFocus;
    }

    [Command]
    public void CmdUsePowerUpOnEnemy(PowerUpType type, NetworkInstanceId nid) {
        UsePowerUp(type, NetworkServer.FindLocalObject(nid));
    }

    [ClientRpc]
    public void RpcUsePowerUpOnEnemy(PowerUpType type, NetworkInstanceId nid) {
        if(!isServer)
            UsePowerUp(type, null);
    }

    [ClientRpc]
    public void RpcUpdateScore(int score1, int score2) {
        GameLogic.Instance.score1 = score1;
        GameLogic.Instance.score2 = score2;
        GameLogic.Instance.UpdateUI();
        SoundManager.Instance.PlayMadePoint(2);
    }

    [ClientRpc]
    public void RpcStartGame() {
        GameStarted();
    }

    [Command]
    public void CmdDestroyObject(NetworkInstanceId nid) {
        NetworkServer.Destroy(NetworkServer.FindLocalObject(nid));
    }

    #endregion
}