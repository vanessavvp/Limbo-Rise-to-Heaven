using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public GameObject cpON, cpOFF;

    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player") {
            GameManager.instance.SetSpawnPoint(transform.position);

            CheckPoint[] allCheckPoints = FindObjectsOfType<CheckPoint>();
            for ( int i = 0; i < allCheckPoints.Length; i++) {
                allCheckPoints[i].cpOFF.SetActive(true);
                allCheckPoints[i].cpON.SetActive(false);
            }
            
            cpOFF.SetActive(false);
            cpON.SetActive(true);
        }
    }
}
