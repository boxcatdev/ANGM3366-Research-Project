using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class SpawnerVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    private Spawner spawner;

    private void Start()
    {
        spawner = GetComponent<Spawner>();
    }

    private void Update()
    {
        if (spawner.canSpawn == true && meshRenderer.material != activeMaterial)
        {
            meshRenderer.material = activeMaterial;
            Debug.Log("Active");
        }
        if (spawner.canSpawn == false && meshRenderer.material != inactiveMaterial)
        {
            meshRenderer.material = inactiveMaterial;
            Debug.Log("Inactive");
        }
    }
}
