using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KartItemHolder : MonoBehaviour
{
    public ItemType? currentItem = null; // null = アイテム無し
    public KeyCode useKey = KeyCode.Space; // 発動キー
    public float decisionDelay = 2f;

    private KartController kart;
    public GameObject bananaPrefab;

    // UI
    public ItemUI itemUI;

    void Awake()
    {
        kart = GetComponent<KartController>();
    }

    void Update()
    {
        if (currentItem != null && Input.GetKeyDown(useKey))
        {
            UseItem(currentItem.Value);
            currentItem = null; // 使用後は空に
            itemUI?.ClearItem();
        }
    }

    // 直接決めるのではなく「抽選開始」に変更
    public void StartItemDecision()
    {
        if (currentItem != null) return; // 既に持ってたら無視
        StartCoroutine(ItemDecisionCoroutine());
    }

    private IEnumerator ItemDecisionCoroutine()
    {
        // UIにルーレット開始
        itemUI?.StartDecisionAnimation(decisionDelay);

        Debug.Log("アイテム抽選中...");
        yield return new WaitForSeconds(decisionDelay);

        // ランダムに決定
        ItemType decidedItem = GetRandomItem();
        currentItem = decidedItem;
        Debug.Log("アイテム決定: " + decidedItem);

        // UIに確定アイテムを渡す
        itemUI?.SetItem(decidedItem.ToString());
    }

    private ItemType GetRandomItem()
    {
        ItemType[] possibleItems = { ItemType.Boost, ItemType.Banana };
        int index = Random.Range(0, possibleItems.Length);
        return possibleItems[index];
    }

    private void UseItem(ItemType item)
    {
        Debug.Log("アイテム発動: " + item);
        switch (item)
        {
            case ItemType.Boost:
                if (kart != null) kart.StartBoost(3f);
                break;
            case ItemType.Banana:
                DropBanana();
                break;
        }
    }

    void DropBanana()
    {
        Vector3 spawnPos = transform.position - transform.forward * 2f;
        Quaternion spawnRot = Quaternion.identity;
        Instantiate(bananaPrefab, spawnPos, spawnRot);
    }
}