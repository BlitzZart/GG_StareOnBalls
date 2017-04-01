using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShowNwManagerHUDToggle : MonoBehaviour {
    private static ShowNwManagerHUDToggle instance;
    NetworkManagerHUD nwmHUD;
    
    private void Awake() {
        instance = this;
    }

    void Start () {
        nwmHUD = FindObjectOfType<NetworkManagerHUD>();
	}
	
    public static void ShowUI(bool val) {
        instance.nwmHUD.showGUI = val;
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            nwmHUD.showGUI = !nwmHUD.showGUI;
	}
}
