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
        //if (Input.GetKeyDown(KeyCode.R)) {
        //    DoRestart();
        //}
    }

    public void DoRestart() {
        NW_Discovery.StopDiscovery();
        NetworkManager nwm = FindObjectOfType<NetworkManager>();
        nwm.StopClient();
        nwm.StopHost();

        StartCoroutine(LoadDelayed());
    }

    private IEnumerator LoadDelayed() {
        yield return new WaitForSeconds(0.333f);
        SceneManager.LoadScene("init");
    }
}