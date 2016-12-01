using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;
using UnityEngine.Networking;

public class MoveOnLook : NetworkBehaviour {
    GazeAware ga;

    NetworkPlayer serverPlayer;

	// Use this for initialization
	void Start () {
        ga = GetComponent<GazeAware>();

        ServerPlayer();
	}

    NetworkPlayer ServerPlayer() {
        NetworkPlayer[] nwp = FindObjectsOfType<NetworkPlayer>();

        if (serverPlayer != null)
            return serverPlayer;

        foreach (NetworkPlayer item in nwp) {
            if (!item.isServer) {
                serverPlayer = item;
                break;
            }
        }
        return serverPlayer;
    }
    

    // Update is called once per frame
    void Update() {
        if (ga.HasGazeFocus) {
            if (!isServer) {
                Debug.LogError("!isServer");
                ServerPlayer().CmdHasFocus(1);
            }

            else {
                Debug.LogError("isServer");
                transform.Translate(0, 0, 1 * Time.deltaTime);
            }

        }
    }



}
