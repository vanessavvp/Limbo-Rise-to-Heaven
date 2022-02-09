using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObject : MonoBehaviour
{
    public GameObject ObjectToPickUp;    
    public GameObject PickedObject;
    public Transform interactionZone;
    public Transform player;
    public GameObject hand;
    private bool isTaken = false;
    public GameObject portal;
    public GameObject canvasController;

    void Update() {
        if (Gamepad.all.Count != 0) {
            if (Gamepad.current.buttonWest.isPressed) {
                /*if (PickedObject != null && isTaken == true) {
                    Drop();
                }*/ 
                if (ObjectToPickUp != null && PickedObject == null && isTaken == false) {
                    PickUp();
                }
            }
        }
    }  

    private void PickUp() {
        portal.SetActive(true);
        hand.SetActive(true);
        canvasController.SetActive(false);
        PickedObject = ObjectToPickUp;
        PickedObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        PickedObject.transform.SetParent(interactionZone);
        PickedObject.transform.position = interactionZone.position;
        PickedObject.transform.rotation = interactionZone.rotation;
        PickedObject.GetComponent<Rigidbody>().useGravity = false;
        PickedObject.GetComponent<Rigidbody>().isKinematic = true;
        isTaken = true;  
    }

    public void Drop() {
        if (PickedObject != null) {
            PickedObject.transform.SetParent(null);
            PickedObject.GetComponent<Rigidbody>().useGravity = true;
            PickedObject.GetComponent<Rigidbody>().isKinematic = false;
            PickedObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            PickedObject = null;
            isTaken = false;
            hand.SetActive(false);
        }
    }
}
