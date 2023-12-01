using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Lumin;
using UnityEngine.Rendering;

public class AttackController : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float hitRange = 2f;
    [SerializeField] private int hitDamage = 1;
    public float attackCooldown = 3f;

    [Header("Attack VFX")]
    [SerializeField] private Transform attackVFX;

    public float cooldownProgress {  get; private set; }
    public bool canAttack {  get; private set; }

    [Header("Unity Events")]
    public UnityEvent OnAttack;

    private void Start()
    {
        //reset cooldown
        cooldownProgress = attackCooldown;
        canAttack = true;
    }
    private void Update()
    {
        #region Cooldown
        if (canAttack == false)
        {
            cooldownProgress -= Time.deltaTime;
            if (cooldownProgress <= 0)
            {
                cooldownProgress = attackCooldown;
                canAttack = true;
            }
        }
        #endregion

        #region Actions
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            TryDamageSpawner();
        }
        #endregion

    }

    private void TryDamageSpawner()
    {
        if (canAttack == false) return;

        //do damage
        foreach (var item in GetHitColliders(transform.position, hitRange))
        {
            Debug.Log(item.transform.name);

            //only if damageable
            if (item.TryGetComponent(out Damageable damageable))
            {
                damageable.Damage(hitDamage);
            }
        }

        //instantiate vfx
        Transform vfx = Instantiate(attackVFX, transform);
        vfx.SetParent(null);

        //trigger cooldown
        canAttack = false;
        OnAttack?.Invoke();
    }
    private Collider[] GetHitColliders(Vector3 position, float range)
    {
        //gets all colliders within range
        Collider[] colliders = Physics.OverlapSphere(position, range);

        return colliders;
    }
}
