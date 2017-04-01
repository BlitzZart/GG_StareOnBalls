using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniScene : MonoBehaviour {
    public string sceneName = "start";
	// Use this for initialization
	void Start () {
        GetComponent<LoadScene>().Load(sceneName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
