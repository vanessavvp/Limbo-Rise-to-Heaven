using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerFireZone : MonoBehaviour
{
    public int ID;

    public void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.tag == "Player") 
        {
            GameEvents.current.FireZoneEnter(ID);
        }
    }

    public void OnPointerEnter(PointerEventData data) 
    {
        GameEvents.current.FireZoneEnter(ID);
    }
}
