﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public void Awake()
    {
        current = this;
    }

    public event Action<int> onFireZoneEnter;

    public void FireZoneEnter(int fireZoneID)
    {
        if (onFireZoneEnter != null)
        {
            onFireZoneEnter(fireZoneID);
        }
    }

}
