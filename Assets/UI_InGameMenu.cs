using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InGameMenu : MonoBehaviour {

    public GameObject menu;

	void Start () {
		
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.Return) && menu.activeSelf) {
            RestartGame.RestartNow();
        }


        if (Input.GetKeyDown(KeyCode.Escape)) {
            menu.SetActive(!menu.activeSelf);
        }

	}
}
