using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KartItemHolder : MonoBehaviour
{
    public ItemType? currentItem = null; // null = アイテム無し
    public KeyCode useKey = KeyCode.Space; // 発動キー

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

    public void SetItem(ItemType item)
    {
        currentItem = item;
        Debug.Log("アイテム所持: " + item);
        // UI 更新
        Debug.Log("item.ToString(): " + item.ToString());
        itemUI?.SetItem(item.ToString());
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