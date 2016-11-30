using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;

public class MoveOnLook : MonoBehaviour {
    GazeAware ga;
	// Use this for initialization
	void Start () {
        ga = GetComponent<GazeAware>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (ga.HasGazeFocus) {
            transform.Translate(0, 0, 1 * Time.deltaTime);
        }
	}
}
