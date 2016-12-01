using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;

public class PowerUpOnHit : MonoBehaviour {
    float currPosX;
    float currPosY;
    float currPosZ;
    float gazeTime;

    private GazeAware _gazeAwareComponent;

	// Use this for initialization
	void Start () {
        currPosX = this.gameObject.transform.position.x;
        currPosY = this.gameObject.transform.position.y;
        currPosZ = this.gameObject.transform.position.z;
        gazeTime = -1;
        _gazeAwareComponent = GetComponent<GazeAware>();
    }
	
	// Update is called once per frame
	void Update () {

        //hide if looked at & "a" pressed
        if (_gazeAwareComponent.HasGazeFocus && Input.GetKeyDown("a"))
        {
            
            Vector3 temp = new Vector3(currPosX + 1.0f, currPosY, currPosZ);
            this.gameObject.transform.position = temp;
            gazeTime = Time.time;
        }

        //Respawn after 5 sec
        if(gazeTime != -1 && gazeTime + 5.0f < Time.time)
        {
            Vector3 temp = new Vector3(currPosX, currPosY, currPosZ);
            this.gameObject.transform.position = temp;
            gazeTime = -1;
        }
    }
}
