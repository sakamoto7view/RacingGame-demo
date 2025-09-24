
using System.Collections;
using UnityEngine;
using Cinemachine;

public class CPUController : MonoBehaviour
{
    public CinemachineDollyCart cart;
    public float startSpeed = 10f; // CPU の走行スピード
    [HideInInspector] public bool canMove = false;

    private bool isSpinning = false;

    void Update()
    {
        if (!isSpinning)
        {
            cart.m_Speed = canMove ? startSpeed : 0f;
        }
        else
        {
            cart.m_Speed = 0f; // スピン中は止める
        }
    }

    public void Spin(float spinDuration = 1f)
    {
        if (isSpinning) return;
        StartCoroutine(SpinCoroutine(spinDuration));
    }

    private IEnumerator SpinCoroutine(float spinDuration)
    {
        isSpinning = true;

        float elapsed = 0f;
        float startAngle = transform.eulerAngles.y;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / spinDuration);
            float angle = Mathf.Lerp(0f, 720f, progress); // 2回転
            transform.rotation = Quaternion.Euler(0f, startAngle + angle, 0f);
            yield return null;
        }

        isSpinning = false;
    }
}