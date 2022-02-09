using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    private Vector3 respawnPosition;

    private void Awake () {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        respawnPosition = FirstPersonController.instance.transform.position;
        StartCoroutine(StartLevel());
        // respawnPosition = new Vector3(-72.3f, 58.6f, 432.3f);
    }

    public void Respawn() {
        StartCoroutine(RespawnWaiter());
        HealthManager.instance.PlayerKilled();
    }

    public IEnumerator RespawnWaiter() {
        // FirstPersonController.instance.gameObject.SetActive(false);
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(2f);
        FirstPersonController.instance.transform.position = respawnPosition;
        StarterAssetsInputs.instance.move = Vector2.zero;
        UIManager.instance.fadeFromBlack = true;
        // FirstPersonController.instance.gameObject.SetActive(true);
        HealthManager.instance.ResetHealth();
    }

    public void SetSpawnPoint(Vector3 newSpawnPosition) {
        respawnPosition = newSpawnPosition;
    }

    public void EnterHeaven() {
        StartCoroutine(EnterHeavenWaiter());
    }

    public IEnumerator EnterHeavenWaiter() {
        // FirstPersonController.instance.gameObject.SetActive(false);
        UIManager.instance.fadeToWhite = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public IEnumerator StartLevel() {
        yield return new WaitForSeconds(1);
        UIManager.instance.fadeFromBlack = true;
        StarterAssetsInputs.instance.move = Vector2.zero;
        // FirstPersonController.instance.gameObject.SetActive(true);
        HealthManager.instance.ResetHealth();
    }

    public IEnumerator EndLevel(int nextScene) {
        // FirstPersonController.instance.gameObject.SetActive(false);
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextScene);
    }
}
