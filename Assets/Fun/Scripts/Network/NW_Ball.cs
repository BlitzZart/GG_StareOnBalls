using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;
using UnityEngine.Networking;

public delegate void BoolDelegate(bool value);
public class NW_Ball : NetworkBehaviour {
    public static event BoolDelegate EventFocusChanged;

    private Rigidbody _body;
    private GazeAware _gaze;
    private MeshDeformer _meshDeformer;
    private bool _mouseIsDown;
    [SyncVar]
    public int number;

    [SyncVar]
    public bool p1HasFocus, p2HasFocus;

    public float speed = 0.1f;
    private float rotationSpeed = 37;


    void Start() {
        _body = GetComponent<Rigidbody>();
        _gaze = GetComponent<GazeAware>();
        _meshDeformer = GetComponent<MeshDeformer>();
    }

    void OnMouseDown() {
        if (Input.GetMouseButton(0)) {
            _mouseIsDown = true;
        }
    }

    void Update() {
        if (Input.GetMouseButtonUp(0))
            _mouseIsDown = false;

        CheckGaze();
        DoMovenent();
    }


    private void CheckGaze() {
        if (_gaze.HasGazeFocus || _mouseIsDown) {
            if (isServer) {
                if (!p1HasFocus)
                    if (EventFocusChanged != null)
                        EventFocusChanged(true);

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

    private void KillForces() {
        if (_body.velocity.sqrMagnitude != 0) {
            print(_body.velocity.sqrMagnitude);
            _body.velocity = Vector3.zero;
        }
    }

    private void DoMovenent() {
        DoMeshDeformation();
        if (!Communicator.Player.isServer)
            return;

        if (p1HasFocus && p2HasFocus) {
            KillForces();
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