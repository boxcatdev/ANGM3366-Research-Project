using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; private set; }

    public static StatsProfile Profile = new StatsProfile();
    //private bool isProfileSetup = false;

    //storage
    public Dictionary<Rarity, int> GlobalInventory { get; private set; }

    //events
    public Action OnGlobalInventoryChanged;

    private void Awake()
    {
        transform.SetParent(null);

        #region Static Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
        #endregion

        #region Stats Singleton
        if(Profile.isProfileSetup == false)
        {
            //Profile = new StatsProfile();
            Profile.ResetProperties(3f, 2f, 0.5f);
            //Profile.ResetProperties(3f, 5f, 3f);
            Debug.LogWarning("ResetProperties()");
        }
        #endregion

        //collectable inventory
        GlobalInventory = new Dictionary<Rarity, int>()
        {
            {Rarity.Common, 0},
            {Rarity.Uncommon, 0},
            {Rarity.Rare, 0},
            {Rarity.UltraRare, 0},
            {Rarity.OneOfAKind, 0},
        };

    }
    private void Start()
    {
        OnGlobalInventoryChanged?.Invoke();
    }

    #region Public Inventory Functions
    public void CombineToGlobalInventory(Dictionary<Rarity, int> addingInventory)
    {
        foreach (var pair in addingInventory)
        {
            GlobalInventory[pair.Key] += addingInventory[pair.Key];
        }

        OnGlobalInventoryChanged?.Invoke();
    }
    public void UpdateGlobalInventory(Dictionary <Rarity, int> playerInventory)
    {
        foreach(var pair in playerInventory)
        {
            GlobalInventory[pair.Key] = playerInventory[pair.Key];
        }
        OnGlobalInventoryChanged?.Invoke();
    }
    public void AddToGlobalInventory(Rarity rarity, int amount)
    {
        GlobalInventory[rarity] += amount;

        OnGlobalInventoryChanged?.Invoke();
    }
    public void RemoveFromGlobalInventory(Rarity rarity, int amount)
    {
        GlobalInventory[rarity] -= amount;
        if (GlobalInventory[rarity] < 0) GlobalInventory[rarity] = 0;

        OnGlobalInventoryChanged?.Invoke();
    }
    public void ClearGlobalInventory()
    {
        foreach (var pair in GlobalInventory)
        {
            GlobalInventory[pair.Key] = 0;
        }

        OnGlobalInventoryChanged?.Invoke();
    }
    #endregion
}
