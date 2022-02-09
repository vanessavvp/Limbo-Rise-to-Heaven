using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenLight : MonoBehaviour {
    Light lt;
    public GameObject player;
    // Start is called before the first frame update
    void Start() {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update() {
        if (Vector3.Distance(transform.position, player.transform.position) <= 65f && lt.range < 30f) {
            lt.range += (10 * Time.deltaTime);
        }
    }
}
