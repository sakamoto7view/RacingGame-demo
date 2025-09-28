using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class Racer
{
    public Transform kartTransform;     // プレイヤーやCPUの実体
    public int lapCount = 0;
    public float progressOnPath = 0f;   // 0~1
    public int rank = 0;
    public float TotalProgress = 0f;    // lapCount + progressOnPath で計算;

    // === 進行度の更新 ===
    public void UpdateProgress(CinemachinePathBase path)
    {
        float posOnPath = path.FindClosestPoint(kartTransform.position, 0, -1, 1000);
        progressOnPath = posOnPath / path.PathLength;

        // 距離ベースの進捗
        float distance = posOnPath; // コース上の位置（単位:メートル）
        TotalProgress = lapCount * path.PathLength + distance;
    }

    // === 順位計算 ===
    public static void UpdateRanks(List<Racer> racers)
    {
        // TotalProgress で降順ソート
        racers.Sort((a, b) => b.TotalProgress.CompareTo(a.TotalProgress));

        // ランクを付与
        for (int i = 0; i < racers.Count; i++)
        {
            racers[i].rank = i + 1;
        }
    }
}