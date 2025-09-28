using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    public float duration = 0.1f;
    public float jumpHeight = 10f;

    public AudioSource coinSE;

    private Vector3 startLocalPos;
    private bool playing = false;
    private float timer = 0f;
    private Renderer rend;

    void Awake()
    {
        startLocalPos = transform.localPosition;
        rend = GetComponent<Renderer>();
        if (rend != null) rend.enabled = false;
    }

    public void Play()
    {
        if (playing) return;
        if (rend != null) rend.enabled = true;
        playing = true;
        timer = 0f;

        // ここでコインSEを再生
        if (coinSE != null)
        {
            coinSE.Play();
        }
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
            if (rend != null) rend.enabled = false;
            playing = false;
            transform.localPosition = startLocalPos;
        }
    }
}