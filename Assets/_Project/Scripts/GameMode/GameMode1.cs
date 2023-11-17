using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode1 : GameModeBase
{
    [Header("Game Mode 1")]
    [SerializeField] private CollectableInventory playerInventory;

    private void Awake()
    {

    }

    public void ScoreRound()
    {
        GameInstance.Instance.CombineToGlobalInventory(playerInventory.collectableDictionary);
    }
}
