using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Text 用

public class CoinUI : MonoBehaviour
{
    public static CoinUI Instance;

    private int coinCount = 0;
    public Text coinText; // UIも直接参照

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;

        // UI更新もここでやっちゃう
        if (coinText != null)
            coinText.text = "× " + coinCount;
    }
}