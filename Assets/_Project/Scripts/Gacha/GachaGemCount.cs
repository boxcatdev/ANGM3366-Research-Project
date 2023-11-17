using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class GachaGemCount : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI commonCount;
    [SerializeField] private TextMeshProUGUI uncommonCount;
    [SerializeField] private TextMeshProUGUI rareCount;
    [SerializeField] private TextMeshProUGUI ultraRareCount;

    private void Start()
    {
        GameInstance.Instance.OnGlobalInventoryChanged += RefreshDisplay;
    }

    private void RefreshDisplay()
    {
        Dictionary<Rarity, int> refInv = GameInstance.Instance.GlobalInventory;

        if (commonCount != null) commonCount.text = refInv[Rarity.Common].ToString("00");
        if (commonCount != null) uncommonCount.text = refInv[Rarity.Uncommon].ToString("00");
        if (commonCount != null) rareCount.text = refInv[Rarity.Rare].ToString("00");
        if (commonCount != null) ultraRareCount.text = refInv[Rarity.UltraRare].ToString("00");

    }
}
