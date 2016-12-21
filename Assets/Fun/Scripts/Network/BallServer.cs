using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Fun;

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
    public bool _gameRunning = false;

    public GameObject[] powerupPrefab;
    public Transform[] spawnAreas;

    private float _minSpawnRate = 3.1f;
    private float _spawnPowerUpRate = 10;
    private float _spawnPowerUpRateRandom = 1;
    private float _spawnPowerUpRateDecrase = 0.333f;

    private Timer _spawnPowerupTimer;

    #region unity callbacks
    void Awake() {
        instance = this;
    }

    void Start() {
        balls = new List<NW_Ball>();
    }

    private void OnLevelWasLoaded(int level) {
        _gameStarted = false;
        _gameRunning = false;
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

    private void OnDestroy() {

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

        _spawnPowerupTimer = Timer.CreateTimer(gameObject, _spawnPowerUpRate, () => { SpawnPowerUp(); });

        if (EventGameStarted != null)
            EventGameStarted();
    }

    private void SpawnPowerUp() {
        GameObject go = Instantiate(powerupPrefab[Random.Range(0, powerupPrefab.Length)]);

        APowerUp currentType = go.GetComponent<APowerUp>();

        //NW_PowerUp pu = go.GetComponent<NW_PowerUp>();
        //pu.Init();

        Transform spawnVolume = spawnAreas[Random.Range(0, spawnAreas.Length)].transform;
        Vector3 pos = spawnVolume.position + new Vector3(Random.Range(-spawnVolume.transform.localScale.x, spawnVolume.transform.localScale.x),
            Random.Range(-spawnVolume.transform.localScale.y, spawnVolume.transform.localScale.y),
            Random.Range(-spawnVolume.transform.localScale.z, spawnVolume.transform.localScale.z));
        go.transform.position = pos;

        NetworkServer.Spawn(go);

        if (_spawnPowerupTimer.interval > _minSpawnRate)
            _spawnPowerupTimer.interval -= _spawnPowerUpRateDecrase;
        else
            _spawnPowerupTimer.interval = _minSpawnRate;

        print("  " + _spawnPowerupTimer.interval);
    }

    #endregion
}