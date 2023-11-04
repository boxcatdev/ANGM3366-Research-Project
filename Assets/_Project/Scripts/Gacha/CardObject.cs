using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObject : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private GameObject cardFront;
    [SerializeField] private GameObject cardBack;

    [Header("Attributes")]
    [SerializeField] private bool flippable;

    [Header("Card Properties")]
    [SerializeField] private Rarity rarity;

    private Animator animator;

    private bool isBack;

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

}
