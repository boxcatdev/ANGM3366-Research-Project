using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class Collectable : MonoBehaviour
{
    public Rarity rarity;
    public int value = 1;
    public bool hasCombined = false;

    public float triggerRange = 1.5f;

    //private Rigidbody rbody;
    private SphereCollider trigger;

    private void Awake()
    {
        //rbody = GetComponent<Rigidbody>();
        trigger = GetComponent<SphereCollider>();
        trigger.radius = triggerRange;
    }
    public void DestroySelf()
    {
        Destroy(this);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.TryGetComponent(out Collectable collectable))
        {
            if(collectable.priority > priority)
            {
                collectable.value = collectable.value + value;
                Destroy(this);
                Destroy(gameObject);
            }
            value += collectable.value;
            
        }*/
        if (other.TryGetComponent(out CollectableInventory inventory))
        {
            inventory.AddToInventory(this);

            DestroySelf();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, triggerRange);
    }
}

public enum Rarity { Common, Uncommon, Rare, UltraRare, OneOfAKind }

