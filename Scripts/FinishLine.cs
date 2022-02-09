using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour {

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            StartCoroutine(FadeOut());
            if (nextScene == SceneManager.sceneCountInBuildSettings) {
                nextScene = 0;
            } else if (nextScene == SceneManager.sceneCountInBuildSettings - 1) {
                GameManager.instance.EnterHeaven();
            } else {
                StartCoroutine(GameManager.instance.EndLevel(nextScene));
            }
        }
    }

    private IEnumerator FadeOut() {
        AudioSource[] songs = FindObjectsOfType<AudioSource>();
        while (songs[0].volume > 0f) {
            foreach (AudioSource song in songs) {
                song.volume -= Time.deltaTime;
            }
            yield return null;
        }
    }
}
