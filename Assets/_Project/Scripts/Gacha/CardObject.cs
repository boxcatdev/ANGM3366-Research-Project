using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Diagnostics;
using UnityEditor.ShaderGraph.Drawing;

public class CardObject : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private GameObject cardFront;
    [SerializeField] private GameObject cardBack;

    [Header("Attributes")]
    [SerializeField] private bool flippable;

    [Header("Card Properties")]
    [SerializeField] private Rarity rarity;

    [Header("Upgrade Properties")]
    //[SerializeField] private UpgradeCost upgradeCost;
    [SerializeField] private Rarity upgradeRarity;
    [SerializeField, Range(1, 10)] private int upgradeCost;
    [SerializeField] private GameObject upgradeButton;

    [Header("Visibility Settings")]
    [SerializeField] private Image CardGemIcon;
    [Space]
    [SerializeField] private TextMeshProUGUI textUpgradeCost;
    [SerializeField] private Image imgUpgradeGem;
    [Space]
    [SerializeField] private List<Color> gemColors = new List<Color>();

    //references
    private Animator animator;

    //private variables
    private bool isBack;

    [Serializable]
    public struct UpgradeCost
    {
        Rarity rarity;
        int amount;
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        DisableAnimator();

        //enable cursor
        var input = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
        if(input != null) input.SetCursorState(false);
    }
    private void Start()
    {
        ChangeToCardBack();
    }

    #region Gacha Stuff
    public void UpgradeCard()
    {
        //check if player has enough to upgrade

        //do vfx for changing card value
        //change card value
        //do vfx to disable upgrade button
        //disable upgrade button

        bool hasCost = true;
        if(hasCost)
        {
            //change card value
            rarity = upgradeRarity;

            //disable upgrade button
            if(upgradeButton != null) upgradeButton.SetActive(false);
        }
    }
    public void BuyUpgrade()
    {
        UnityEngine.Debug.Log("BuyUpgrade()");

        //check if inventory contains enough to upgrade
        //remove cost from inventory and add new
        //add more of that gem into player's inventory

        Dictionary<Rarity, int> inventoryRef = GameInstance.Instance.GlobalInventory;

        if (inventoryRef[upgradeRarity] >= upgradeCost)
        {
            //has enough to upgrade

            GameInstance.Instance.RemoveFromGlobalInventory(upgradeRarity, upgradeCost);

            GameInstance.Instance.AddToGlobalInventory(rarity, 1);

            UnityEngine.Debug.Log(GameInstance.Instance.GlobalInventory[rarity]);
        }
    }
    #endregion

    #region Animation Functions
    private void DisableAnimator()
    {
        animator.enabled = false;
    }
    private void FlipCard()
    {
        if (!flippable) return;

        animator.enabled = true;
        animator.Play("CardFlip");
    }
    private void ChangeToCardBack()
    {
        if (cardFront != null && cardBack != null)
        {
            cardFront.SetActive(false);
            cardBack.SetActive(true);
            isBack = true;
        }
    }
    private void ChangeToCardFront()
    {
        if (cardFront != null && cardBack != null)
        {
            cardFront.SetActive(true);
            cardBack.SetActive(false);
            isBack = false;
        }
    }
    #endregion

    #region Visibility Functions
    private void Update()
    {
        //change Gem Icon color
        if (rarity == Rarity.Common && CardGemIcon.color != gemColors[0]) CardGemIcon.color = gemColors[0];
        if (rarity == Rarity.Uncommon && CardGemIcon.color != gemColors[1]) CardGemIcon.color = gemColors[1];
        if (rarity == Rarity.Rare && CardGemIcon.color != gemColors[2]) CardGemIcon.color = gemColors[2];
        if (rarity == Rarity.UltraRare && CardGemIcon.color != gemColors[3]) CardGemIcon.color = gemColors[3];

        //change Gem Cost color
        if (upgradeRarity == Rarity.Common && imgUpgradeGem.color != gemColors[0]) imgUpgradeGem.color = gemColors[0];
        if (upgradeRarity == Rarity.Uncommon && imgUpgradeGem.color != gemColors[1]) imgUpgradeGem.color = gemColors[1];
        if (upgradeRarity == Rarity.Rare && imgUpgradeGem.color != gemColors[2]) imgUpgradeGem.color = gemColors[2];
        if (upgradeRarity == Rarity.UltraRare && imgUpgradeGem.color != gemColors[3]) imgUpgradeGem.color = gemColors[3];

        //change Gem Cost amount
        if(textUpgradeCost.text != upgradeCost.ToString()) textUpgradeCost.text = upgradeCost.ToString();

        //switch between flippable and not
        if(flippable && upgradeButton.activeInHierarchy) upgradeButton.SetActive(false);
        if (!flippable && !upgradeButton.activeInHierarchy) upgradeButton.SetActive(true);
    }
    #endregion

}
