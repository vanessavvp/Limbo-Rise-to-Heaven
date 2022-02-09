using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private Transform fireZonePosition;
    public GameObject fireZone;
    private GameObject player;
    public int ID;

    private void Start()
    {
        if (fireZone != null)
            GameEvents.current.onFireZoneEnter += OnFireZoneEnable;
    }

    /* Enables fire zone when the event is triggered by near player */
    public void OnFireZoneEnable(int ID)
    {
        if ((!fireZone.activeSelf) && (ID == this.ID) && (fireZone != null))
        {
            fireZone.SetActive(true);
        }
    }
}
