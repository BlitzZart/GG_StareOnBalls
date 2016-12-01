using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    public float speed = 1.0f;

    #region unity callbacks
    void Start () {
        if (!isServer && hasAuthority) {
            Transform camTrans = Camera.main.transform;
            camTrans.rotation = Quaternion.Euler(50, 180, 0);
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y, -camTrans.position.z);
        }
	}
	
	void Update () {
	
	}
    #endregion

    #region private
    private void UsePowerUp(PowerUpType type) {
        
    }
    #endregion

    #region network
    [Command]
    public void CmdHasFocus(int ballNumber) {
        BallServer.Instace.balls[ballNumber].transform.Translate(0, 0, -speed * Time.deltaTime);
    }

    [Command]
    public void CmdPowerUp(PowerUpType type) {
        UsePowerUp(type);
    }
    #endregion
}
