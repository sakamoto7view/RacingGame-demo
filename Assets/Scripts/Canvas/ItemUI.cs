using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image itemImage;        // Canvas に置いた Image をアタッチ
    public Sprite bananaSprite;    // Banana 用スプライト
    public Sprite boostSprite;     // Boost 用スプライト

    // 追加：Inspectorで割り当てるAudioSourceとクリップ
    public AudioSource audioSource;       // AudioSource本体
    public AudioClip rouletteClip;        // ルーレット中SE（ループ再生）
    public AudioClip decisionClip;        // 決定SE（1回だけ）

    private Coroutine decisionRoutine;
    private ItemType? currentItem = null;
    void Awake()
    {
        itemImage.enabled = false;
    }

    // アイテムを取得したときに呼ぶ
    public void SetItem(ItemType item)
    {
        currentItem = item;
        // ルーレットを強制停止
        if (decisionRoutine != null)
        {
            StopCoroutine(decisionRoutine);
            decisionRoutine = null;
        }
        // ルーレットSEを止める
        if (audioSource != null && rouletteClip != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        if (itemImage != null)
        {
            itemImage.enabled = true; // 表示を有効化
            // 確定表示
            UpdateDisplay();
        }

        // 決定SEを鳴らす
        if (audioSource != null && decisionClip != null)
        {
            audioSource.PlayOneShot(decisionClip);
        }
    }

    private void UpdateDisplay()
    {
        if (itemImage == null) return;

        switch (currentItem)
        {
            case ItemType.Banana:
                itemImage.sprite = bananaSprite;
                break;
            case ItemType.Boost:
                itemImage.sprite = boostSprite;
                break;
            default:
                itemImage.sprite = null;
                break;
        }
    }

    // ルーレット開始
    public void StartDecisionAnimation(float duration = 2f, float interval = 0.1f)
    {
        if (itemImage != null)
            itemImage.enabled = true;

        if (decisionRoutine != null)
            StopCoroutine(decisionRoutine);

        decisionRoutine = StartCoroutine(DecisionAnimation(duration, interval));

        // ルーレットSEをループ再生
        if (audioSource != null && rouletteClip != null)
        {
            audioSource.clip = rouletteClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private IEnumerator DecisionAnimation(float duration, float interval)
    {
        float elapsed = 0f;
        bool toggle = false;

        while (elapsed < duration)
        {
            itemImage.sprite = toggle ? bananaSprite : boostSprite;
            toggle = !toggle;

            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }

        decisionRoutine = null;

        // ルーレットSEを停止
        if (audioSource != null && audioSource.isPlaying && audioSource.clip == rouletteClip)
        {
            audioSource.Stop();
        }
    }

    // アイテムを使ったら呼ぶ
    public void ClearItem()
    {
        currentItem = null;
        if (itemImage != null)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }
    }
}