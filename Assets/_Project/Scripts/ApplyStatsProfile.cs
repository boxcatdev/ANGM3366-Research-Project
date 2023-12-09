using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStatsProfile : MonoBehaviour
{
    private AttackController attackController;
    private ThirdPersonController playerController;
    private SphereCollider sphereCollider;

    private void Start()
    {
        //get components
        attackController = GetComponent<AttackController>();
        playerController = GetComponent<ThirdPersonController>();
        sphereCollider = GetComponent<SphereCollider>();

        //Debug.Log("Apply Profile");
        //Debug.LogFormat("[Profile] C:{0} S:{1} R:{2}", GameInstance.Profile.cooldownTime, GameInstance.Profile.playerSpeed, GameInstance.Profile.collectionRange);
        //apply profile
        attackController.attackCooldown = GameInstance.Profile.cooldownTime;
        playerController.MoveSpeed = GameInstance.Profile.playerSpeed;
        playerController.SprintSpeed = GameInstance.Profile.playerSpeed * (8 / 3);
        sphereCollider.radius = GameInstance.Profile.collectionRange;
    }
}
