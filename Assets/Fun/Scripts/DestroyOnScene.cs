using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnScene : MonoBehaviour {

    public string sceneName;

    private void OnLevelWasLoaded(int level) {
        if (SceneManager.GetActiveScene().name == sceneName)
            Destroy(this.gameObject);
    }
}
