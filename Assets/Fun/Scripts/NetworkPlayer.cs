using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    public float speed = 1.0f;

    #region unity callbacks
    void Start() {
        if (!isServer && hasAuthority) {
            Transform camTrans = Camera.main.transform;
            camTrans.rotation = Quaternion.Euler(50, 180, 0);
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y, -camTrans.position.z);
        }
    }

    void Update() {

    }
    #endregion

    #region public
    public void UsePowerUp(PowerUpType type) {
        PowerUpFactory.ActivatePowerUp(type);
    }
    #endregion

    #region network
    [Command]
    public void CmdHasFocus(int ballNumber) {
        BallServer.Instace.balls[ballNumber].transform.Translate(0, 0, -speed * Time.deltaTime);
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
    #endregion
}