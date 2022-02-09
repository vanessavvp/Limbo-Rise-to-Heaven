using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            HealthManager.instance.Hurt();
        }
    }
}
