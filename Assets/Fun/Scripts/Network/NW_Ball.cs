using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;
using UnityEngine.Networking;

public class NW_Ball : NetworkBehaviour {
    private GazeAware _gaze;
    private MeshDeformer _meshDeformer;
    private bool _mouseIsDown;
    [SyncVar]
    public int number;

    [SyncVar]
    public bool p1HasFocus, p2HasFocus;

    public float speed = 0.1f;
    private float rotationSpeed = 37;

    // Use this for initialization
    void Start() {
        _gaze = GetComponent<GazeAware>();
        _meshDeformer = GetComponent<MeshDeformer>();
    }

    void OnMouseDown() {
        if (Input.GetMouseButton(0)) {
            _mouseIsDown = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonUp(0))
            _mouseIsDown = false;

        CheckGaze();
        DoMovenent();
    }

    private void CheckGaze() {
        if (_gaze.HasGazeFocus || _mouseIsDown) {
            if (isServer) {
                p1HasFocus = true;
            }
            else if (!p2HasFocus) {
                Communicator.Player.CmdHasFocus(number, true);
            }
        }
        else {
            if (isServer) {
                p1HasFocus = false;
            }
            else if (p2HasFocus) {
                Communicator.Player.CmdHasFocus(number, false);
            }
        }
    }

    private void DoMovenent() {
        DoMeshDeformation();
        if (!Communicator.Player.isServer)
            return;

        if (p1HasFocus && p2HasFocus) {
        }
        else if (p1HasFocus) {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (p2HasFocus) {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
    }

    private void DoMeshDeformation() {
        if (p1HasFocus && p2HasFocus) {
            _meshDeformer.power = 7;
        }
        else if (p1HasFocus || p2HasFocus) {
            _meshDeformer.power = 3;
        } else {
            _meshDeformer.power = 0;
        }
    }
}