using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI collectableUI;

    List<Collectable> collectables;
    Dictionary<Rarity, int> collectableDictionary;

    public Action<Dictionary<Rarity, int>> OnInventoryChanged;

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

        collectableDictionary = new Dictionary<Rarity, int>()
        {
            { Rarity.Common, 0 },
            { Rarity.Uncommon, 0 },
            { Rarity.Rare, 0 },
            { Rarity.UltraRare, 0 },
            { Rarity.OneOfAKind, 0 },

        };

        RefreshCollectableUI();
    }
    public void AddToInventory(Collectable collectable)
    {
        collectableDictionary[collectable.rarity] += collectable.value;

        Debug.LogFormat("Rarity: {0}, Amount: {1}", collectable.rarity.ToString(), collectableDictionary[collectable.rarity]);

        OnInventoryChanged?.Invoke(collectableDictionary);
        RefreshCollectableUI();

        #region List Method
        /*if (!collectables.Contains(collectable))
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
        }*/
        #endregion
    }
    public void RemoveFromInventory(Rarity rarity, int amount)
    {
        collectableDictionary[rarity] -= amount;
        if (collectableDictionary[rarity] < 0) collectableDictionary[rarity] = 0;

        Debug.LogFormat("Rarity: {0}, Amount: {1}", rarity.ToString(), collectableDictionary[rarity]);

        OnInventoryChanged?.Invoke(collectableDictionary);
        RefreshCollectableUI();
    }
    public int GetAmountInInventory(Rarity rarity)
    {
        return collectableDictionary[rarity];
    }
    #endregion

    #region UI

    private void RefreshCollectableUI()
    {
        if(collectableUI != null)
        {
            int commonScore = collectableDictionary[Rarity.Common];
            int uncommonScore = collectableDictionary[Rarity.Uncommon];
            int rareScore = collectableDictionary[Rarity.Rare];
            int ultrarareScore = collectableDictionary[Rarity.UltraRare];

            string formattedText = "Common: " + commonScore + 
                                    " \nUncommon: " + uncommonScore + 
                                    " \nRare: " + rareScore + 
                                    " \nUltraRare: " + ultrarareScore;

            collectableUI.text = formattedText;
        }
    }

    #endregion
}
