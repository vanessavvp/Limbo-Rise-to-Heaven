using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Ward : MonoBehaviour
{
    public GameObject location;
    public GameObject player;

    private void OnTriggerEnter(Collider collider) {
        StartCoroutine(Teleport());
    }

    public void Respawn() {
        
    }

    public IEnumerator Teleport() {
        // FirstPersonController.instance.gameObject.SetActive(false);
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(2f);
        player.transform.position = location.transform.position;
        StarterAssetsInputs.instance.move = Vector2.zero;
        UIManager.instance.fadeFromBlack = true;
        // FirstPersonController.instance.gameObject.SetActive(true);
    }
}
