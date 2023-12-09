using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
//[RequireComponent(typeof(Collectable))]
public class CollectableVisuals : MonoBehaviour
{
    private Collectable collectable;

    [Header("References")]
    [SerializeField] private MeshRenderer meshRenderer;
    //[SerializeField] private Light light;
    [Header("Materials")]
    [SerializeField] private Material commonMaterial;
    [SerializeField] private Material uncommonMaterial;
    [SerializeField] private Material rareMaterial;
    [SerializeField] private Material ultraRareMaterial;
    //[SerializeField] private Material oneOfAKindMaterial;


    private void Awake()
    {
        collectable = GetComponent<Collectable>();
    }
    private void Start()
    {
        if (meshRenderer != null)
        {
            if (collectable.rarity == Rarity.Common && meshRenderer.material != commonMaterial)
                meshRenderer.material = commonMaterial;
            if (collectable.rarity == Rarity.Uncommon && meshRenderer.material != uncommonMaterial)
                meshRenderer.material = uncommonMaterial;
            if (collectable.rarity == Rarity.Rare && meshRenderer.material != rareMaterial)
                meshRenderer.material = rareMaterial;
            if (collectable.rarity == Rarity.UltraRare && meshRenderer.material != ultraRareMaterial)
                meshRenderer.material = ultraRareMaterial;
        }
    }
    /*private void Update()
    {
        if(meshRenderer != null)
        {
            if (collectable.rarity == Rarity.Common && meshRenderer.material != commonMaterial)
                meshRenderer.material = commonMaterial;
            if (collectable.rarity == Rarity.Uncommon && meshRenderer.material != uncommonMaterial)
                meshRenderer.material = uncommonMaterial;
            if (collectable.rarity == Rarity.Rare && meshRenderer.material != rareMaterial)
                meshRenderer.material = rareMaterial;
            if (collectable.rarity == Rarity.UltraRare && meshRenderer.material != ultraRareMaterial)
                meshRenderer.material = ultraRareMaterial;
        }
    }*/
}
