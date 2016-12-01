using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    public Transform testBall;

    private MoveOnLook[] balls;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Command]
    public void CmdHasFocus(int ballNumber) {
        Debug.LogError("cmdHasFocus");
        testBall = FindObjectOfType<MoveOnLook>().transform;
        testBall.Translate(0, 0, 1 * Time.deltaTime);
    }
}
