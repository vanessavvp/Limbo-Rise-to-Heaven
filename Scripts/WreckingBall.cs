using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class WreckingBall : MonoBehaviour {
    public GameObject player;
    private Rigidbody rb;

    void Start() {
        rb = transform.parent.gameObject.GetComponent<Rigidbody>();
    } 

    private void OnTriggerEnter(Collider collision) {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Player") {
            Debug.Log("YES!");
            Vector3 pos = player.GetComponent<Transform>().position;
            if (pos.x < transform.position.x) {
                rb.AddForce(-transform.right * 20f);
            } else {
                rb.AddForce(transform.right * 20f);
            }
            player.GetComponent<FirstPersonController>().KnockBack();
            HealthManager.instance.Hurt();
        }
    }
}
