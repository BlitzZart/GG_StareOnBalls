using UnityEngine;
using System.Collections;

public class skybox_rotator : MonoBehaviour {
    private Material skybox;

    // Use this for initialization
    void Start () {
        skybox = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update () {
        float asd = skybox.GetFloat("_Rotation");
        skybox.SetFloat("_Rotation", asd + 0.1f);
    }
}
