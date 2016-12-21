using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;


// always implement activation and deactivation in one method
// because it gets called again for deactivation - automatically
public class PowerUpFactory : MonoBehaviour {
    private MonoBehaviour _skyboxRotator, _edgeDetection, _grayscale, _blur;
    private float duration = 4.7f;

    private static PowerUpFactory _instance;

    #region unity callbacks
    void Awake() {
        _instance = this;
    }
    void Start() {
        Initialize();
    }
    #endregion

    #region public
    public static void ActivatePowerUp(PowerUpType type) {
        _instance.SetPowerUp(type, true);
    }
    #endregion

    #region private
    private void SetPowerUp(PowerUpType type, bool enable) {
        switch (type) {
            case PowerUpType.SkyboxRotator:
                MonoBehaviorActivator(_skyboxRotator, type, enable);
                break;
            case PowerUpType.EdgeDetection:
                MonoBehaviorActivator(_edgeDetection, type, enable);
                break;
            case PowerUpType.GreyScale:
                MonoBehaviorActivator(_grayscale, type, enable);
                break;
            case PowerUpType.FlipCamera:
                FlipCamera(type, enable);
                break;
            case PowerUpType.Blur:
                MonoBehaviorActivator(_blur, type, enable);
                break;
        }
    }

    private void Initialize() {
        _skyboxRotator = FindObjectOfType<SkyboxRotator>();
        _edgeDetection = FindObjectOfType<EdgeDetection>();
        _grayscale = FindObjectOfType<Grayscale>();
        _blur = FindObjectOfType<Blur>();
    }

    private void MonoBehaviorActivator(MonoBehaviour comp, PowerUpType type, bool enable) {
        //Debug.LogError("POWERUP " + type.ToString() + " > " + enable);
        if (enable) {
            comp.enabled = true;
            StartCoroutine(StopPowerUp(type));
        } else {
            comp.enabled = false;
        }
    }

    int rotCamDir = 1;
    private void FlipCamera(PowerUpType type, bool flip) {
        Transform camTrans = Camera.main.transform;
        Vector3 eulerRot = camTrans.transform.rotation.eulerAngles;
        if (flip) {
            camTrans.transform.rotation = Quaternion.Euler(eulerRot.x, eulerRot.y, 180);
            rotCamDir = -rotCamDir;
            StartCoroutine(StopPowerUp(type));
        } else {
            camTrans.transform.rotation = Quaternion.Euler(eulerRot.x, eulerRot.y, 0);
        }
    }

    private IEnumerator StopPowerUp(PowerUpType type) {
        yield return new WaitForSeconds(duration);
        SetPowerUp(type, false);
    }

    #endregion
}