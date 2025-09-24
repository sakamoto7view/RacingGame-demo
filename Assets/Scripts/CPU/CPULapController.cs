

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPULapController : MonoBehaviour
{
    private Rigidbody rb;
    public int lapCount = 0;
    [HideInInspector] public bool passedGoal = false;
    [HideInInspector] public Vector3 lastGoalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void AddLap()
    {
        lapCount++;
        Debug.Log("CPU Lap: " + lapCount);
    }

    void Update()
    {
        // ゴールから離れたら再カウント可能にする
        if (passedGoal && Vector3.Distance(transform.position, lastGoalPosition) > 5f)
        {
            passedGoal = false;
        }
    }

    public void StopGradually(float duration)
    {
        StartCoroutine(StopRoutine(duration));
    }
    private IEnumerator StopRoutine(float duration)
    {
        float time = 0f;
        Vector3 initialVelocity = rb.velocity;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            rb.velocity = Vector3.Lerp(initialVelocity, Vector3.zero, t);
            yield return null;
        }

        rb.velocity = Vector3.zero;
    }
}