using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {

	void Start () {
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            if (Input.GetKey(KeyCode.AltGr) || 
                Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || 
                Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand))
                SceneManager.LoadScene("funfunfun");
        }
	}
}
