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
    }
    #endregion

    #region public
    public void UsePowerUp(PowerUpType type) {
        PowerUpFactory.ActivatePowerUp(type);
    }
    #endregion

    #region private

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
    public void CmdUsePowerUpOnEnemy(PowerUpType type) {
        UsePowerUp(type);
    }

    [ClientRpc]
    public void RpcUsePowerUpOnEnemy(PowerUpType type) {
        if(!isServer)
            UsePowerUp(type);
    }

    [ClientRpc]
    public void RpcUpdateScore(int score1, int score2) {
        GameLogic.Instance.score1 = score1;
        GameLogic.Instance.score2 = score2;
        GameLogic.Instance.UpdateUI();
    }

    #endregion
}