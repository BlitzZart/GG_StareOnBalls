using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxis : MonoBehaviour {
    public Vector3 axis;

    void Update() {
        transform.Rotate(axis.x, axis.y, axis.z);
    }
}
