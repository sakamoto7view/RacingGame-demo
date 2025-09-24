using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CPUFollower : MonoBehaviour {
    public CinemachineDollyCart cart;

    void Update()
    {
        // 現在位置
        Vector3 currentPos = cart.m_Path.EvaluatePositionAtUnit(cart.m_Position, cart.m_PositionUnits);

        // 進行方向（少し先の位置との差分）
        Vector3 nextPos = cart.m_Path.EvaluatePositionAtUnit(cart.m_Position + 0.1f, cart.m_PositionUnits);
        Vector3 forward = (nextPos - currentPos).normalized;

        // 進行方向に向ける
        if (forward.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(forward, Vector3.up) * Quaternion.Euler(0, 0, 0);
        }
    }
}