using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    public float boostDuration = 1.5f;
    private void OnTriggerEnter(Collider other)
    {
        KartController kart = other.GetComponent<KartController>();
        if (kart != null)
        {
            kart.StartBoost(boostDuration);
        }
    }
}
