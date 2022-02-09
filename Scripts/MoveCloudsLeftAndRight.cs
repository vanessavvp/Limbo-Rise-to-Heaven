using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class MoveCloudsLeftAndRight : MonoBehaviour {
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public float distance = 5f;
    public float speed = 4f;
    public GameObject player;
    private Vector3 lastPosition;
    public enum direction {Left, Right}
    [Header("Choose Start Direction")]
    public direction Directions;
    // Start is called before the first frame update
    void Start() {
        initialPosition = transform.position;
        targetPosition = transform.position;
        if (Directions == direction.Left) {
            targetPosition.x -= distance;
        } else {
            targetPosition.x += distance;
        }
        lastPosition = initialPosition;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.x == initialPosition.x + distance) {
            targetPosition.x = initialPosition.x - distance;
        } else if (transform.position.x == initialPosition.x - distance) {
            targetPosition.x = initialPosition.x + distance;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            player.GetComponent<FirstPersonController>().onPlatform(speed, (transform.position - lastPosition));
            lastPosition = transform.position;
        }
    }
}
