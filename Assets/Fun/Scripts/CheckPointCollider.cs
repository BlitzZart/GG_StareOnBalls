using UnityEngine;
using System.Collections;

public class CheckPointCollider : MonoBehaviour {
    public delegate void CheckPoint(int playerNumber);
    public static event CheckPoint EventMadePoint;

    public int playerNumber = 0;

    void OnTriggerEnter(Collider other) {
        //if (!Communicator.Player.isServer)
        //    return;

        Destroy(other.transform.parent.gameObject);

        if (EventMadePoint != null) {
            EventMadePoint(playerNumber);
        }
    }
}