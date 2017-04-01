using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetwrokKiller : MonoBehaviour {

	void Start () {
        NetworkManager nwm = FindObjectOfType<NetworkManager>();

        if (nwm != null)
            Destroy(nwm.gameObject);
	}
}
