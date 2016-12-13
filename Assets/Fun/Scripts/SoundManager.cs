using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip focusOn;
    public AudioSource sfxSource, musicSource;

    private float _rndRange = 0.2f;

	void Start () {
        sfxSource = GetComponent<AudioSource>();

        NW_Ball.EventFocusChanged += OnFocusChanged;
        BallServer.EventGameStarted += OnGameStarted;
	}
	
	void OnDestroy () {
        NW_Ball.EventFocusChanged -= OnFocusChanged;
        BallServer.EventGameStarted -= OnGameStarted;
    }

    private void OnGameStarted() {
        musicSource.Play();
    }

    private void OnFocusChanged(bool value) {
        if (value) {
            sfxSource.clip = focusOn;
            sfxSource.pitch = 1 + Random.Range(-_rndRange, _rndRange);
            sfxSource.Play();
        }
    }
}
