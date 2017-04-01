using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class RestartGame : MonoBehaviour {
    private static RestartGame instance;
    void Awake() {
        instance = this;
    }

    public static void RestartNow() {
        instance.DoRestart();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            DoRestart();
        }
    }

    private void DoRestart() {
        NetworkManager nwm = FindObjectOfType<NetworkManager>();
        nwm.StopClient();
        nwm.StopHost();

        SceneManager.LoadScene("start");
    }
}