using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; private set; }

    //storage
    public Dictionary<Rarity, int> GlobalInventory { get; private set; }
    private void Awake()
    {
        #region Static Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion

        //collectable inventory
        GlobalInventory = new Dictionary<Rarity, int>();
    }

    #region Public Inventory
    public void AddToGlobalInventory(Dictionary<Rarity, int> addingInventory)
    {
        foreach (var pair in addingInventory)
        {
            GlobalInventory[pair.Key] += addingInventory[pair.Key];
        }
    }
    public void ClearGlobalInventory()
    {
        foreach (var pair in GlobalInventory)
        {
            GlobalInventory[pair.Key] = 0;
        }
    }
    #endregion
}
