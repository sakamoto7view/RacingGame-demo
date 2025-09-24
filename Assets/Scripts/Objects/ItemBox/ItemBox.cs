using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KartItemHolder holder = other.GetComponent<KartItemHolder>();
            // プレイヤーにアイテム付与
            if (holder != null)
            {
                holder.StartItemDecision();
            }
            // 消えるのはエフェクトが終わった後
            Destroy(gameObject);
        }
    }
}
