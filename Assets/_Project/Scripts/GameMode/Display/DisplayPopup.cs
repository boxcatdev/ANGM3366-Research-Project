using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPopup : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] CollectableInventory inventory;

    [Header("Display")]
    [SerializeField] private TextMeshProUGUI commonText;
    [SerializeField] private TextMeshProUGUI uncommonText;
    [SerializeField] private TextMeshProUGUI rareText;
    [SerializeField] private TextMeshProUGUI ultraRareText;

    private void Awake()
    {
        inventory.OnInventoryChanged += RefreshValues;
    }

    private void RefreshValues(Dictionary<Rarity, int> values)
    {
        Debug.Log("Refresh Display Popup");
        
        commonText.text = values[Rarity.Common].ToString("00");
        uncommonText.text = values[Rarity.Uncommon].ToString("00");
        rareText.text = values[Rarity.Rare].ToString("00");
        ultraRareText.text = values[Rarity.UltraRare].ToString("00");
    }
}
