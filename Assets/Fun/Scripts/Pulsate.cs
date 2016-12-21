using UnityEngine;
using System.Collections;

public class Pulsate : MonoBehaviour {

	public float scaleRange = 0.08f;
	public float pulseFreq = 10f;

	private float minScale;
	private float maxScale;
	private float scaleTime = 0f;

	void Start() {
		minScale = transform.localScale.x * (1-scaleRange/2);
		maxScale = transform.localScale.x * (1+scaleRange/2);
	}

	// Update is called once per frame
	void Update () {
		scaleTime += Time.unscaledDeltaTime * pulseFreq * 2 * Mathf.PI;
		float alpha = (Mathf.Sin(scaleTime) + 1) / 2;
		float scale = Mathf.Lerp(minScale, maxScale, alpha);
		transform.localScale = new Vector3(scale, scale, scale);
	}
}
