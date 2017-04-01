using UnityEngine;

public class SoundManager : MonoBehaviour {
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get { return instance; }
    }

    public AudioClip focusOn, makePoint;
    public AudioSource musicSource;

    private AudioSource[] sfxSources;

    private float _rndRange = 0.17f;

    private void Awake() {
        instance = this;
    }

    void Start () {
        sfxSources = GetComponents<AudioSource>();

        NW_Ball.EventFocusChanged += OnFocusChanged;
        //BallServer.EventGameStarted += OnGameStarted;
        CheckPointCollider.EventMadePoint += PlayMadePoint;
    }
	
	void OnDestroy () {
        NW_Ball.EventFocusChanged -= OnFocusChanged;
        //BallServer.EventGameStarted -= OnGameStarted;
        CheckPointCollider.EventMadePoint -= PlayMadePoint;
    }

    public void PlayMadePoint(int playerNumber) {
        sfxSources[1].clip = makePoint;
        sfxSources[1].pitch = 1 + Random.Range(-_rndRange, _rndRange);
        sfxSources[1].Play();
    }

    public void StartMusic() {
        musicSource.Play();
    }

    public void OnFocusChanged(bool value) {
        if (value) {
            sfxSources[0].clip = focusOn;
            sfxSources[0].pitch = 1 + Random.Range(-_rndRange, _rndRange);
            sfxSources[0].Play();
        }
    }
}
