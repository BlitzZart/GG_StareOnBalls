using UnityEngine;
using System.Collections;

public class SkyboxRotator : MonoBehaviour {
    private Material skybox;

    private float _speed = 0.1f;
    private float _randomSpeed = 0;
    private float _direction = 1;

    // Use this for initialization
    void Start () {
        skybox = RenderSettings.skybox;
	}

    private void OnEnable() {
        _randomSpeed = Random.Range(0.03f, 0.3f);
        _direction = -_direction;
    }

    // Update is called once per frame
    void Update () {
        float asd = skybox.GetFloat("_Rotation");
        skybox.SetFloat("_Rotation", asd + ((_speed + _randomSpeed) * _direction));
    }
}
