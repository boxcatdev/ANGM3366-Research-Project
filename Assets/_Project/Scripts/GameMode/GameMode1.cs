using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode1 : GameModeBase
{
    private Dictionary<Rarity, int> storedCollectables;
    private void Awake()
    {
        storedCollectables = new Dictionary<Rarity, int>();
    }

    public void StoreCollectables(Dictionary<Rarity, int> collectableInventory)
    {
        storedCollectables.Clear();

        foreach (var rarity in collectableInventory)
        {
            storedCollectables[rarity.Key] = collectableInventory[rarity.Key];
        }
    }
}
