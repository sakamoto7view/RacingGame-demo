using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤー
        KartController kart = other.GetComponent<KartController>();
        if (kart != null)
        {
            kart.Spin(stopDuration: 0.5f, spinDuration: 1f);
            Destroy(gameObject); // 踏まれたらバナナは消える
            return;
        }

        // CPU
        CPUController cpu = other.GetComponentInParent<CPUController>();
        if (cpu != null)
        {
            cpu.Spin(spinDuration: 1f);
            Destroy(gameObject); // 踏まれたらバナナは消える
        }
    }
}