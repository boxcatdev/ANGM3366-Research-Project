using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [SerializeField] private float hitRange = 2f;
    [SerializeField] private int hitDamage = 1;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            TryDamageSpawner();
        }
    }

    private void TryDamageSpawner()
    {
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
    }
    private Collider[] GetHitColliders(Vector3 position, float range)
    {
        //gets all colliders within range
        Collider[] colliders = Physics.OverlapSphere(position, range);

        return colliders;
    }
}
