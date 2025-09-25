using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Text 用

public class RankUI : MonoBehaviour
{
    public RaceManager raceManager;
    public Text rankText;
    public Text finishRankText;

    void Update()
    {
        if (raceManager != null && raceManager.playerRacer != null)
        {
            int rank = raceManager.playerRacer.rank;
            int total = raceManager.racers.Count;
            rankText.text = $"{rank}";
            finishRankText.text = $"{rank}位";
        }
    }
}
