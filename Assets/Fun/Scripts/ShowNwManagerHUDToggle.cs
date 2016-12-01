using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ShowNwManagerHUDToggle : MonoBehaviour {
    NetworkManagerHUD nwmHUD;
	// Use this for initialization
	void Start () {
        nwmHUD = FindObjectOfType<NetworkManagerHUD>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            nwmHUD.showGUI = !nwmHUD.showGUI;
	}
}
