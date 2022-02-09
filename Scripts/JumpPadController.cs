using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class JumpPadController : MonoBehaviour {
    public GameObject player;
    public float jumpHeight = 6f;


    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Capsule") {
            player = collision.gameObject.transform.parent.gameObject;
            player.GetComponent<FirstPersonController>().Jumping(jumpHeight); 
        }
    }

}
