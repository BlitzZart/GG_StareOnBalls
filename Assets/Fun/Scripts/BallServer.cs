using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class BallServer : NetworkBehaviour {
    private static BallServer instance;
    public static BallServer Instace
    {
        get { return instance; }
    }

    public List<MoveOnLook> balls;
    public GameObject ballPrefab;
    bool started = false;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        balls = new List<MoveOnLook>();
    }


	// Update is called once per frame
	void Update () {
	    if (!started && Input.GetKeyDown(KeyCode.Space)) {
            started = true;
            StartUpHost();
        }
	}

    public void SpawnObject(GameObject toSpawn, Vector3 pos) {
        GameObject go = (GameObject)Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        NetworkServer.Spawn(go);
    }

    private void StartUpHost() {
        for (int i = 0; i < 5; i++) {
            GameObject obj = Instantiate(ballPrefab, ballPrefab.transform.position + i * Vector3.right * 3.5f, Quaternion.identity) as GameObject;

            MoveOnLook nb = obj.GetComponent<MoveOnLook>();
            nb.number = i;
            balls.Add(nb);

            NetworkServer.Spawn(obj);
        }
    }
}
