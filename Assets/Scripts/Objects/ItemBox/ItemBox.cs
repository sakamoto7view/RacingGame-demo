using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public ItemType[] possibleItems;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KartItemHolder holder = other.GetComponent<KartItemHolder>();
            // プレイヤーにアイテム付与
            if (holder != null && possibleItems.Length > 0)
            {
                ItemType selected = possibleItems[Random.Range(0, possibleItems.Length)];
                holder.SetItem(selected);
            }
            // 消えるのはエフェクトが終わった後
            Destroy(gameObject);
        }
    } // Inspectorで設定可能

    void GiveRandomItem(GameObject player)
    {
        if (possibleItems.Length == 0) return;

        // ランダムで1つ選ぶ
        ItemType selected = possibleItems[Random.Range(0, possibleItems.Length)];

        switch (selected)
        {
            case ItemType.Boost:
                // プレイヤーにブースト付与
                KartController kart = player.GetComponent<KartController>();
                if (kart != null) kart.StartBoost(2f);
                break;

            case ItemType.Banana:
                // 例: 次の短距離加速
                // 好きな処理を追加
                break;
        }

        Debug.Log("ItemBox: " + selected + " を取得");
    }
}
