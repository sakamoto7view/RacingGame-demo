


using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public KartController player;
    public CPULapController cpu;
    public CPUController cpuMover;
    public int maxLaps = 1;

    public CinemachinePathBase racePath;
    public List<Racer> racers = new List<Racer>();
    public Racer playerRacer;

    private bool raceFinished = false;
    public GameObject finishUI;
    public GameObject finishRankUI;
    public GameObject retryButton;
    public GameObject playerRankingUI;

    public AudioSource bgmSource;      // ゴール時のBGM再生用
    public AudioSource finishBGM;        // 再生するクリップ
    public AudioSource clapBGM;        // 再生するクリップ
    public AudioSource yeahBGM;        // 再生するクリップ

    public int coinCount = 0; // 現在のコイン数

    void Start()
    {
        playerRacer = racers.Find(r => r.kartTransform == player.transform);
    }

    void Update()
    {
        if (!raceFinished)
        {
            // レース進行中のみ進捗と順位更新
            foreach (var r in racers) r.UpdateProgress(racePath);
            Racer.UpdateRanks(racers);

            // ゴールチェック
            if (player.lapCount >= maxLaps || cpu.lapCount >= maxLaps)
            {
                CheckRaceEnd();
            }
        }
    }

    public void CheckRaceEnd()
    {
        if (raceFinished) return;

        if (player.lapCount >= maxLaps || cpu.lapCount >= maxLaps)
        {
            raceFinished = true;

            // ゴール直後に順位を確定させる
            foreach (var r in racers) r.UpdateProgress(racePath);
            Racer.UpdateRanks(racers);

            StartCoroutine(EndRace());
        }
    }

    private IEnumerator EndRace()
    {
        Debug.Log("Race Finished!");

        if (finishUI != null && retryButton != null)
        {
            playerRankingUI.SetActive(false);
            finishUI.SetActive(true);
            finishRankUI.SetActive(true);
            retryButton.SetActive(true);
        }

        if (bgmSource != null && finishBGM != null && clapBGM != null && yeahBGM != null)
        {
            bgmSource.Stop();
            finishBGM.Play();
            clapBGM.Play();
            yeahBGM.Play();
        }

        // 3秒待ってから停止処理開始
        yield return new WaitForSeconds(3f);

        // プレイヤー減速停止
        player.StopGradually(3f);

        // CPU減速停止
        if (cpu != null)
        {
            cpu.StopGradually(3f);
        }
        else if (cpuMover != null)
        {
            cpuMover.canMove = false;
        }

        // ここで順位を表示する処理を呼ぶのもアリ
        foreach (var r in racers)
        {
            Debug.Log($"{r.name}: Rank {r.rank}");
        }
    }

    // コインを加算するメソッド
    public void AddCoin(int amount)
    {
        coinCount += amount;
        Debug.Log("コイン数: " + coinCount);

        // TODO: UI更新処理をここで呼んでもOK
    }

    public void RestartRace()
    {
        // 現在のシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}