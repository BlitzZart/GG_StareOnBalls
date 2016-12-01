using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallServer : NetworkBehaviour {
    private static BallServer instance;
    public static BallServer Instace
    {
        get { return instance; }
    }

    public MoveOnLook[] balls;
    public GameObject ballPrefab;
    bool started = false;

    void Awake() {
        instance = this;
    }

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

            MoveOnLook nb = obj.GetComponent<MoveOnLook>();
            nb.number = i;

            NetworkServer.Spawn(obj);
        }
    }
}
