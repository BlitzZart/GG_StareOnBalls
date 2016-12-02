using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PowerUpFactory : MonoBehaviour {
    private MonoBehaviour _skyboxRotator, _edgeDetection, _grayscale;
    private float duration = 4;

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
        }
    }

    private void Initialize() {
        _skyboxRotator = FindObjectOfType<SkyboxRotator>();
        _edgeDetection = FindObjectOfType<EdgeDetection>();
        _grayscale = FindObjectOfType<Grayscale>();
    }

    private void MonoBehaviorActivator(MonoBehaviour comp, PowerUpType type, bool enable) {
        Debug.LogError("POWERUP " + type.ToString() + " > " + enable);
        if (enable) {
            comp.enabled = true;
            StartCoroutine(StopPowerUp(type));
        } else {
            comp.enabled = false;
        }
    }


    private IEnumerator StopPowerUp(PowerUpType type) {
        yield return new WaitForSeconds(duration);
        SetPowerUp(type, false);
    }

    #endregion
}