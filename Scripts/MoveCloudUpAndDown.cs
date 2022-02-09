using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloudUpAndDown : MonoBehaviour {

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public float speed = 4f;
    // Start is called before the first frame update
    void Start() {
        initialPosition = transform.position;
        targetPosition = transform.position;
        targetPosition.y += 5f;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y == initialPosition.y + 5f) {
            targetPosition.y = initialPosition.y - 2f;
        } else if (transform.position.y == initialPosition.y - 2f) {
            targetPosition.y = initialPosition.y + 5f;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
