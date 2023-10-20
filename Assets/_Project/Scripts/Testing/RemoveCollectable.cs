using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCollectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CollectableInventory inventory))
        {
            inventory.RemoveFromInventory(Rarity.Common, 1);

            Destroy(gameObject);
        }
    }
}
