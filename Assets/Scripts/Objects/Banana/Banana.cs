using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    public AudioClip crashClip;
    private void OnTriggerEnter(Collider other)
    {
        if (crashClip != null)
        {
            GameObject tmp = new GameObject("TempSE");
            tmp.transform.position = transform.position;
            AudioSource tmpSource = tmp.AddComponent<AudioSource>();
            tmpSource.clip = crashClip;
            tmpSource.Play();
            Destroy(tmp, crashClip.length); // 再生後に削除
        }
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