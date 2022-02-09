using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public PickUpObject PickUpObject;
    public void OnPointerEnter() {
        if (this.gameObject.tag == "pickableObject") {
            PickUpObject.ObjectToPickUp = this.gameObject;
        }
    }

    public void OnPointerExit() {
        if (this.gameObject.tag == "pickableObject") {
            PickUpObject.ObjectToPickUp = null;
        }
    }
}
