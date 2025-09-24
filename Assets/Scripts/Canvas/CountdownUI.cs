using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Text 用
// TextMeshPro を使う場合は using TMPro; に変更

public class CountdownUI : MonoBehaviour
{
    public Text countdownText;        // UI の Text (または TMP_Text)
    public KartController kart;       // カートの参照
    public CPUController[] cpus;      // 複数 CPU に対応
    public int countdownTime = 5;     // 秒数（5秒）

    public AudioSource bgmSource;         // BGM専用
    public AudioSource countdownSource;         // カウント音専用
    bool hasPlayedSound = false;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        int count = countdownTime;

        while (count > 0)
        {
            if (count <= 3)
            {
                countdownText.text = count.ToString();
                if (!hasPlayedSound && countdownSource != null)
                {
                    countdownSource.Play();
                    hasPlayedSound = true; // 一度だけ再生
                }
            }
            else
            {
                countdownText.text = ""; // 4, 5秒の間は非表示
            }

            yield return new WaitForSeconds(1f);
            count--;
        }

        // GO!
        countdownText.text = "GO!";
        kart.canMove = true;

        foreach (var cpu in cpus)
        {
            cpu.canMove = true;
        }

        yield return new WaitForSeconds(1f);

        if (bgmSource != null)
        {
            bgmSource.Play();
        }

        // 表示消す
        countdownText.text = "";
    }
}