using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallServer : NetworkBehaviour {
    private BallServer instance;
    //public BallServer Instace


    public GameObject ballPrefab;
    bool started = false;

    

    // Use this for initialization
    void Start () {


    }


	// Update is called once per frame
	void Update () {
	    if (!started && Input.GetKeyDown(KeyCode.Space)) {
            started = true;
            StartUpHost();
        }
	}


    private void StartUpHost() {

        for (int i = 0; i < 5; i++) {
            GameObject obj = Instantiate(ballPrefab, ballPrefab.transform.position + i * Vector3.right * 3.5f, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(obj);
        }
    }
}
