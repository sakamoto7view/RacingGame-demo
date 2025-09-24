using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxRotate : MonoBehaviour
{
    public float rotateSpeed = 50f; // 回転スピード

    void Update()
    {
        // Y軸回転だけでクルクル
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
