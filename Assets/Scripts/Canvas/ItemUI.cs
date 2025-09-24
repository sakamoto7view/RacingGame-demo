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
        UpdateDisplay();
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
                itemText.text = "⚡";
                break;
            default:
                itemText.text = "";
                break;
        }
    }
}