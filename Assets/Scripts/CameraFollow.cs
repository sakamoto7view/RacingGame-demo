using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // 追従対象（Kart）
    public Vector3 offset = new Vector3(0, 3, -6); // カメラの位置オフセット
    public float followSpeed = 5f;  // 位置の追従スピード
    public float rotationSpeed = 3f; // 回転の追従スピード
    public float maxRotationAngle = 45f; // カメラが曲がれる最大角度

    private void FixedUpdate()
    {
        if (!target) return;

        // 位置の追従
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // 向きの追従（制限あり）
        Quaternion desiredRotation = Quaternion.LookRotation(target.forward, Vector3.up);

        // 現在の回転と目標回転の差分を制限
        Quaternion limitedRotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, maxRotationAngle);

        transform.rotation = Quaternion.Slerp(transform.rotation, limitedRotation, rotationSpeed * Time.deltaTime);
    }
}