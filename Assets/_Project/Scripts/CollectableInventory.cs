using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInventory : MonoBehaviour
{
    List<Collectable> collectables;

    Dictionary<Rarity, int> collectableDictionary;

    #region Monobehavior
    private void Awake()
    {
        ResetInventory();
    }
    #endregion

    #region Inventory
    public void ResetInventory()
    {
        collectables = new List<Collectable>();
        collectables.Clear();

        collectableDictionary = new Dictionary<Rarity, int>();
        collectableDictionary.Add(Rarity.Common, 0);
        collectableDictionary.Add(Rarity.Uncommon, 0);
        collectableDictionary.Add(Rarity.Rare, 0);
        collectableDictionary.Add(Rarity.UltraRare, 0);
        collectableDictionary.Add(Rarity.OneOfAKind, 0);
    }
    public void AddToInventory(Collectable collectable)
    {
        collectableDictionary[collectable.rarity] += collectable.value;

        Debug.LogFormat("Rarity: {0}, Amount: {1}", collectable.rarity.ToString(), collectableDictionary[collectable.rarity]);

        #region List Method
        if (!collectables.Contains(collectable))
            collectables.Add(collectable);

        //Debug.LogFormat("Rarity: {0}, Amount: {1}", collectable.rarity.ToString(), collectable.value);

        //check list if collectables with similar rarity are already in the list
        if(collectables.Count > 0)
        {
            //int count = 0;
            foreach (var item in collectables)
            {
                if (collectable.rarity == item.rarity)
                {
                    item.value += collectable.value;
                }
                else
                {

                }
                
            }
        }
        else
        {
            collectables.Add(collectable);
        }
        #endregion
    }
    public void RemoveFromInventory(Rarity rarity, int amount)
    {
        collectableDictionary[rarity] -= amount;
        if (collectableDictionary[rarity] < 0) collectableDictionary[rarity] = 0;

        Debug.LogFormat("Rarity: {0}, Amount: {1}", rarity.ToString(), collectableDictionary[rarity]);
    }
    public int GetAmountInInventory(Rarity rarity)
    {
        return collectableDictionary[rarity];

        /*if(collectables.Count > index)
        {
            return collectables[index];
        }
        else
        {
            Debug.LogWarning("Index Out of Range of Inventory");
            return null;
        }*/
    }
    #endregion
}
