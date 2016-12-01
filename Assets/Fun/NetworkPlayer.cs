using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    public Transform testBall;

    private MoveOnLook[] balls;

	// Use this for initialization
	void Start () {
        if (isClient) {
            Transform camTrans = Camera.main.transform;
            camTrans.rotation = Quaternion.Euler(50, 180, 0);
            camTrans.position = new Vector3(camTrans.position.x, camTrans.position.y, -camTrans.position.z);
        }


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    public void CmdHasFocus(int ballNumber) {
        testBall = FindObjectOfType<MoveOnLook>().transform;
        testBall.Translate(0, 0, 1 * Time.deltaTime);
    }
}
