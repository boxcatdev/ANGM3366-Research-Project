using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using static CardObjectUpgrade;
using UnityEditor.iOS.Xcode;
using Unity.VisualScripting;

public class CardObjectGrind : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private GameObject cardFront;
    [SerializeField] private GameObject cardBack;

    [Header("Card Properties")]
    [SerializeField] private Rarity rarity;
    [SerializeField, Range(1, 1000)] private int cardCost;

    [Header("Visibility Settings")]
    [SerializeField] TextMeshProUGUI costText;

    [Header("Multipliers")]
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI rangeText;
    [Space]
    [SerializeField] private float cooldownMultiplier;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float rangeMultiplier;

    //references
    private Animator animator;

    //private variables
    private bool isBack;
    private bool canFlip;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        DisableAnimator();
    }
    private void Start()
    {
        ChangeToCardBack();
        RefreshCostText();
        CanAffordCard(cardCost);
        SetupMultiplierTexts();
    }

    #region Gacha stuff
    public void BuyCard()
    {
        //change to check if can afford
        if(CanAffordCard(cardCost))
        {
            GameInstance.Instance.RemoveFromGlobalInventory(Rarity.Common, cardCost);
            FlipCard();
        }
    }
    private bool CanAffordCard(int cost)
    {
        Dictionary<Rarity, int> inventoryRef = GameInstance.Instance.GlobalInventory;

        if(inventoryRef[Rarity.Common] >= cost)
            canFlip = true;
        else
            canFlip = false;

        Debug.LogFormat("Dictionary: {0}, Cost: {1}", inventoryRef[Rarity.Common], cost);

        return canFlip;
    }

    //multipliers
    private void SetupMultiplierTexts()
    {
        if (cooldownText != null) cooldownText.text = "-" + Mathf.Abs(cooldownMultiplier) + "%";
        if (speedText != null) speedText.text = "+" + Mathf.Abs(speedMultiplier) + "%";
        if (rangeText != null) rangeText.text = "+" + Mathf.Abs(rangeMultiplier) + "%";
    }
    public void ApplyCardMultiplier()
    {
        GameInstance.Profile.ApplyMultiplier(Mathf.Abs(cooldownMultiplier), PropertyType.Cooldown);
        GameInstance.Profile.ApplyMultiplier(Mathf.Abs(speedMultiplier), PropertyType.Speed);
        GameInstance.Profile.ApplyMultiplier(Mathf.Abs(rangeMultiplier), PropertyType.Range);
    }

    #endregion

    #region Animation Functions
    private void DisableAnimator()
    {
        animator.enabled = false;
    }
    private void EnableAnimator()
    {
        animator.enabled = true;
    }
    private void FlipCard()
    {
        EnableAnimator();
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

    #region Visibility
    private void RefreshCostText()
    {
        if(costText != null) costText.text = cardCost.ToString();
    }
    #endregion
}
