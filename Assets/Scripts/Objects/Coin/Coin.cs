using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // コイン加算
            if (CoinUI.Instance != null)
            {
                CoinUI.Instance.AddCoin(coinValue);
            }

            // プレイヤーに仕込んである CoinEffect を再生
            CoinEffect effect = other.GetComponentInChildren<CoinEffect>();
            if (effect != null)
            {
                effect.Play();
            }

            // 消えるのはエフェクトが終わった後
            Destroy(gameObject);
        }
    }

}