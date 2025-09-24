using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    // public AudioClip collectSound;

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

    // // コイン本体を少し遅れて削除
    // IEnumerator DestroyAfterSeconds(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     Destroy(gameObject);
    // }
    // IEnumerator PopAndDestroy(GameObject effect, Transform playerTransform)
    // {
    //     float duration = 0.6f;
    //     float jumpHeight = 2f;
    //     float t = 0f;

    //     while (t < duration)
    //     {
    //         t += Time.deltaTime;
    //         float progress = t / duration;

    //         // プレイヤー位置に追従しつつY軸だけ上下
    //         Vector3 pos = playerTransform.position + Vector3.up * 1.5f + Vector3.up * Mathf.Sin(progress * Mathf.PI) * jumpHeight;
    //         effect.transform.position = pos;

    //         // 回転させる場合
    //         effect.transform.Rotate(Vector3.up * 360f * Time.deltaTime);

    //         yield return null;
    //     }

    //     Destroy(effect);
    // }
}