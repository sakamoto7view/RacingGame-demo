using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{ 
    public float speed = 20f;
    public float turnSpeed = 80f;
    private Rigidbody rb;

    // === ブースト関連 ===
    private bool isBoosting = false;
    private float boostEndTime = 0f;
    public float boostMultiplier = 2f; // ブースト中はスピード2倍

    // === ブースト音 ===
    public AudioSource boostSource;  // 音を鳴らす用

    // === ゴールライン関連 ===
    public int lapCount = 0;
    [HideInInspector] public bool passedGoal = false;
    [HideInInspector] public Vector3 lastGoalPosition;

    // カウントダウンで切り替え
    [HideInInspector] public bool canMove = false;

    // === 停止関連 ===
    private bool isStopping = false;
    private float stopStartTime;
    private float stopDuration = 1f; // デフォルトは1秒で停止
    private float stopStartSpeed;

    //バナナのスピンアニメーション
    private bool isSpinning = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        float moveInput = isSpinning ? 0f : Input.GetAxis("Vertical");
        float turnInput = isSpinning ? 0f : Input.GetAxis("Horizontal");

        // ブースト中なら speed を強化
        float currentSpeed = speed;
        if (isBoosting && Time.time < boostEndTime)
        {
            currentSpeed *= boostMultiplier;
        }
        else if (isBoosting && Time.time >= boostEndTime)
        {
            isBoosting = false;
        }

        // === 停止処理 ===
        if (isStopping)
        {
            float t = (Time.time - stopStartTime) / stopDuration;
            if (t >= 1f)
            {
                speed = 0f;
                isStopping = false;
                canMove = false;
            }
            else
            {
                speed = Mathf.Lerp(stopStartSpeed, 0f, t);
            }
        }

        float move = moveInput * currentSpeed * Time.fixedDeltaTime;

        // 前進
        rb.MovePosition(rb.position + transform.forward * move);

        // 回転（スピン中は入力回転させない）
        if (!isSpinning)
        {
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
        }

        // 再カウント可能かチェック
        if (passedGoal && Vector3.Distance(transform.position, lastGoalPosition) > 5f)
        {
            passedGoal = false;
        }
    }

    // === BoostPad から呼び出す ===
    public void StartBoost(float duration)
    {
        isBoosting = true;
        boostSource.Play();
        boostEndTime = Time.time + duration;
        if (boostSource != null)
        {
            StartCoroutine(PlayBoostSound(2f)); // 2秒間再生
        }
    }
    private IEnumerator PlayBoostSound(float playTime)
    {
        boostSource.Play();
        yield return new WaitForSeconds(playTime);
        boostSource.Stop();
    }
    public void AddLap()
    {
        lapCount++;
        Debug.Log("Lap: " + lapCount);
    }

    public void Spin(float stopDuration = 0.5f, float spinDuration = 1f)
    {
        if (isSpinning) return;
        isSpinning = true;
        rb.isKinematic = true;
        StartCoroutine(SpinCoroutine(stopDuration, spinDuration));
    }

    private IEnumerator SpinCoroutine(float stopDuration, float spinDuration)
    {
        float originalSpeed = speed;

        // 1. 停止
        speed = 0f;
        // yield return new WaitForSeconds(stopDuration);

        // 2. 回転（経過時間に応じて絶対角度を設定）
        float elapsed = 0f;
        float startAngle = transform.eulerAngles.y;
        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / spinDuration);
            float angle = Mathf.Lerp(0f, 720f, progress); // 0→360度
            transform.rotation = Quaternion.Euler(0f, startAngle + angle, 0f);
            yield return null;
        }

        // 3. 復帰
        rb.isKinematic = false;
        isSpinning = false;
        speed = originalSpeed;
    }

    // === 外部から呼び出し（例: ゴール後に停止など）===
    public void StopGradually(float duration = 1f)
    {
        Debug.Log("Stopping...");
        if (!isStopping)
        {
            isStopping = true;
            stopStartTime = Time.time;
            stopDuration = duration;
            stopStartSpeed = speed;
        }
    }
}