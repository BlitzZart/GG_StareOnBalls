using UnityEngine;
using System.Collections;

public class CheckPointCollider : MonoBehaviour {
    public delegate void CheckPoint(int playerNumber);
    public static event CheckPoint EventMadePoint;
    public ParticleSystem _particles;

    public int playerNumber = 0;

    void OnTriggerEnter(Collider other) {
        //if (!Communicator.Player.isServer)
        //    return;

        if (_particles != null)
            Instantiate(_particles, other.transform.position, _particles.transform.rotation);
        Destroy(other.transform.parent.gameObject);

        if (EventMadePoint != null) {
            EventMadePoint(playerNumber);
        }
    }
}