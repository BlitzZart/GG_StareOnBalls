using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;
using UnityEngine.Networking;

public class MoveOnLook : NetworkBehaviour {
    GazeAware ga;

    [SyncVar]
    public int number;

    // Use this for initialization
    void Start() {
        ga = GetComponent<GazeAware>();
    }


    // Update is called once per frame
    void Update() {
        if (ga.HasGazeFocus) {
            if (!isServer) {
                Communicator.Player.CmdHasFocus(number);
            }

            else {
                transform.Translate(0, 0, 1 * Time.deltaTime);
            }
        }
    }
}