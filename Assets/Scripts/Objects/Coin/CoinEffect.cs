using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    public float duration = 0.1f;
    public float jumpHeight = 10f;

    private Vector3 startLocalPos;
    private bool playing = false;
    private float timer = 0f;

    void Awake()
    {
        startLocalPos = transform.localPosition;
    }

    public void Play()
    {
        if (playing) return;
        playing = true;
        timer = 0f;
    }

    void Update()
    {
        if (!playing) return;

        timer += Time.deltaTime;
        float progress = timer / duration;

        // パラボラ運動（sinで上下）
        float height = Mathf.Sin(progress * Mathf.PI) * jumpHeight;
        transform.localPosition = startLocalPos + Vector3.up * height;

        if (progress >= 0.8f)
        {
            playing = false;
            transform.localPosition = startLocalPos;
        }
    }
}