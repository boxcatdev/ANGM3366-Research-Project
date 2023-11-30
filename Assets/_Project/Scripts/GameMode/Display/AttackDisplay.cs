using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackDisplay : MonoBehaviour
{
    [SerializeField] private AttackController attackController;
    [SerializeField] private Image attackFill;
    [SerializeField] private TextMeshProUGUI attackText;


    private void Update()
    {
        //text behavior
        if(attackController.canAttack == true && attackText.gameObject.activeInHierarchy == false)
            attackText.gameObject.SetActive(true);
        if(attackController.canAttack == false && attackText.gameObject.activeInHierarchy == true)
            attackText.gameObject.SetActive(false);

        //fill behavior
        if(attackController.cooldownProgress == attackController.attackCooldown && attackFill.fillAmount != 1)
            attackFill.fillAmount = 1;
        if(attackController.cooldownProgress != attackController.attackCooldown)
            attackFill.fillAmount = 1 - (attackController.cooldownProgress / attackController.attackCooldown);
    }
}
