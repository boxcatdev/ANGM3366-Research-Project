using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Collectable : MonoBehaviour
{
    public Rarity rarity;
    public int value = 1;

    public float triggerRange = 1.5f;

    private Rigidbody rbody;
    private SphereCollider trigger;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        trigger = GetComponent<SphereCollider>();
        trigger.radius = triggerRange;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CollectableInventory inventory))
        {
            inventory.AddToInventory(this);

            Destroy(gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, triggerRange);
    }
}

public enum Rarity { Common, Uncommon, Rare, UltraRare, OneOfAKind }

