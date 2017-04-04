using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGameMenu : MonoBehaviour {

    public GameObject menu;

	void Start () {
		
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Communicator.Player == null || Communicator.Player.isServer)
                menu.SetActive(!menu.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.Return) && menu.activeSelf) {
            RestartGame.RestartNow();
        }
    }
}
