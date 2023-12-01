using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaDebug : MonoBehaviour
{
    public void GiveCommonGems(int amount)
    {
        GameInstance.Instance.AddToGlobalInventory(Rarity.Common, amount);
    }
}
