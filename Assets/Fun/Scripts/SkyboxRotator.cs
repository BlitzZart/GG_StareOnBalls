using UnityEngine;
using System.Collections;

public class SkyboxRotator : MonoBehaviour {
    public float fixedSpeed = 0;

    private Material skybox;

    private float _speed = 0.1f;
    private float _randomSpeed = 0;
    private float _direction = 1;

    // Use this for initialization
    void Start () {
        skybox = RenderSettings.skybox;
	}

    private void OnEnable() {
        _randomSpeed = Random.Range(10.0f, 20.0f);
        _direction = -_direction;
    }

    // Update is called once per frame
    void Update () {
        float currentRotation = skybox.GetFloat("_Rotation");
        if (fixedSpeed == 0) {

            skybox.SetFloat("_Rotation", currentRotation + ((_speed + _randomSpeed * Time.deltaTime) * _direction));
        } else {
            skybox.SetFloat("_Rotation", currentRotation + ((_speed + fixedSpeed * Time.deltaTime) * _direction));
        }

    }
}
