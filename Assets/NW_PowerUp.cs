using Assets.Fun;
using System.Collections;
using System.Collections.Generic;
using Tobii.EyeTracking;
using UnityEngine;
using UnityEngine.Networking;

public class NW_PowerUp : NetworkBehaviour {
    private GazeAware _gaze;
    private bool _mouseIsDown;

    private float _lifeTime = 2.7f;

    private APowerUp _puwerUp;

    public Color positivePower, negativePower;

    private void OnNetworkInstantiate(NetworkMessageInfo info) {
        Init();
    }

    void Start () {
        _gaze = GetComponent<GazeAware>();

        // initialize on start if client
        if (!Communicator.Player.isServer) {

        }
        Init();

    }

    void OnMouseDown() {
        if (Input.GetMouseButton(0)) {
            _mouseIsDown = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonUp(0))
            _mouseIsDown = false;

        CheckGaze();
    }

    public void Init() {
        ChangeColor _colorChanger = GetComponent<ChangeColor>();
        MeshRenderer _meshRenderer = GetComponent<MeshRenderer>();
        _puwerUp = GetComponentInChildren<APowerUp>();
        if (_puwerUp.IsABadPowerUp) {
            _colorChanger.SetDeselectionColor(negativePower);
        }
        else {
            _colorChanger.SetDeselectionColor(positivePower);
        }

        Destroy(gameObject, _lifeTime);
    }

    private void CheckGaze() {
        if (_gaze.HasGazeFocus || _mouseIsDown) {
            _puwerUp.Activate();
        }
    }
}
