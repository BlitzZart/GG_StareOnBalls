using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class BallServer : NetworkBehaviour {
    public delegate void BallServerDelegate();
    public static event BallServerDelegate EventGameStarted;

    private static BallServer instance;
    public static BallServer Instace
    {
        get { return instance; }
    }

    public MonoBehaviour startGameText;

    public float ballSpeed = 1.7f;
    public List<NW_Ball> balls;
    public GameObject ballPrefab;
    public GameObject gameLogicPrefab;
    public bool _gameStarted = false;

    #region unity callbacks
    void Awake() {
        instance = this;
    }

    void Start () {
        balls = new List<NW_Ball>();
    }

    private void OnLevelWasLoaded(int level) {
        _gameStarted = false;
        startGameText = FindObjectOfType<UI_StartText>().GetComponent<Text>();
    }

    void Update() {
        if (!_gameStarted) {
            if (Communicator.Player != null && Communicator.Player.isServer) {
                startGameText.enabled = true;
                if (Input.GetKeyDown(KeyCode.Space)) {
                    _gameStarted = true;

                    StartUpHost();
                }
            }
            if (_gameStarted) {
                StartCoroutine(StartGameDelayed());
            }
        }
    }
    #endregion

    #region public
    public void SpawnObject(GameObject toSpawn, Vector3 pos) {
        GameObject go = (GameObject)Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        NetworkServer.Spawn(go);
    }
    #endregion

    #region private
    private void StartUpHost() {
        GameObject gl = Instantiate(gameLogicPrefab);
        NetworkServer.Spawn(gl);

        for (int i = 0; i < 5; i++) {
            GameObject obj = Instantiate(ballPrefab, ballPrefab.transform.position + i * Vector3.right * 3.5f, Quaternion.identity) as GameObject;

            NW_Ball nb = obj.GetComponent<NW_Ball>();
            nb.speed = ballSpeed;
            nb.number = i;
            balls.Add(nb);

            NetworkServer.Spawn(obj);
        }
        startGameText.enabled = false;
    }

    IEnumerator StartGameDelayed() {
        yield return new WaitForSeconds(0.5f);
        if (EventGameStarted != null)
            EventGameStarted();
    }

    #endregion
}
