using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NW_Setup : MonoBehaviour {

    public static bool isAuto;

	void Start () {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level) {
        NetworkManager nwm = FindObjectOfType<NetworkManager>();

        if (nwm == null)
            return;

        if (isAuto) {
            nwm.gameObject.GetComponent<NetworkManagerHUD>().enabled = false;
            nwm.gameObject.GetComponent<ShowNwManagerHUDToggle>().enabled = false;
            nwm.gameObject.GetComponent<NetworkDiscovery>().enabled = true;
        } else {
            nwm.gameObject.GetComponent<NetworkManagerHUD>().enabled = true;
            nwm.gameObject.GetComponent<ShowNwManagerHUDToggle>().enabled = true;
            nwm.gameObject.GetComponent<NetworkDiscovery>().enabled = false;
        }

        Destroy(gameObject);
    }

    public void SetManual() {
        isAuto = false;
    }

    public void SetAuto() {
        isAuto = true;
    }
}