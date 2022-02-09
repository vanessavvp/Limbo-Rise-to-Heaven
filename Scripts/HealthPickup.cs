using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class HealthPickup : MonoBehaviour {
    public int healAmount = 1;
    public bool isFullHeal; 
    public GameObject heartEffect;

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            Destroy(gameObject);
            Instantiate(heartEffect, FirstPersonController.instance.transform.position, FirstPersonController.instance.transform.rotation);
            if (isFullHeal) {
                HealthManager.instance.ResetHealth();
            } else {
                HealthManager.instance.AddHealth(healAmount);
            }
        }
    }
}
