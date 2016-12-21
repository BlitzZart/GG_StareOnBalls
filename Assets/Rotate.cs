using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    float angle;
    float speed = 0.7f;

	void Start () {
        angle = Random.Range(-speed, speed);
	}
	
	void Update () {
        transform.Rotate(angle, angle, angle);
	}
}
