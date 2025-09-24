using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Text 用

public class ItemUI : MonoBehaviour
{
    public Text itemText; // Canvas に置いた Text をアタッチ

    private string currentItem = "";

    // アイテムを取得したときに呼ぶ
    public void SetItem(string itemName)
    {
        Debug.Log("SetItem: " + itemName);
        currentItem = itemName;
        // ルーレットを強制停止
        if (decisionRoutine != null)
        {
            StopCoroutine(decisionRoutine);
            decisionRoutine = null;
        }
        // 確定表示
        UpdateDisplay();
    }
    private Coroutine decisionRoutine;

    public void SetDecisionText(string text)
    {
        Debug.Log("Decision text: " + text);
        itemText.text = text;
    }

    // ルーレット開始
    public void StartDecisionAnimation(float duration = 2f, float interval = 0.1f)
    {
        if (decisionRoutine != null)
            StopCoroutine(decisionRoutine);

        decisionRoutine = StartCoroutine(DecisionAnimation(duration, interval));
    }

    private IEnumerator DecisionAnimation(float duration, float interval)
    {
        float elapsed = 0f;
        bool toggle = false;

        while (elapsed < duration)
        {
            SetDecisionText(toggle ? "B" : "C");
            toggle = !toggle;
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
        itemText.text = "";
        decisionRoutine = null;
    }

    // アイテムを使ったら呼ぶ
    public void ClearItem()
    {
        currentItem = "";
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (itemText == null) return;

        switch (currentItem)
        {
            case "Banana":
                itemText.text = "B";
                break;
            case "Boost":
                itemText.text = "C";
                break;
            default:
                itemText.text = "";
                break;
        }
    }
}