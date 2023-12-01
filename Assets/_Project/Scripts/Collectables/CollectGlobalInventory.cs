using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollectableInventory))]
public class CollectGlobalInventory : MonoBehaviour
{
    private CollectableInventory inventory;

    private void Start()
    {
        inventory = GetComponent<CollectableInventory>();

        inventory.AddToInventory(Rarity.Common, GameInstance.Instance.GlobalInventory[Rarity.Common]);
    }
}
