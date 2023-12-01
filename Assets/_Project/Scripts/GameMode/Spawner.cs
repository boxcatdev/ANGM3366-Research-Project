using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPrefab;
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float minRadius = 1f;
    [SerializeField] private float spawnCooldown = 5f;

    public float cooldownProgress {  get; private set; }
    public bool canSpawn { get; private set; }

    private Damageable damageable;

    [Header("Unity Events")]
    public UnityEvent OnCooldownComplete;

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }
    private void Start()
    {
        //SpawnItems();

        //reset cooldown
        cooldownProgress = spawnCooldown;
        canSpawn = true;
    }
    private void Update()
    {
        #region Spawn Cooldown
        if(canSpawn == false)
        {
            cooldownProgress -= Time.deltaTime;
            if(cooldownProgress <= 0)
            {
                cooldownProgress = spawnCooldown;
                canSpawn = true;
                OnCooldownComplete?.Invoke();
            }
        }
        #endregion
    }

    public void SpawnItems()
    {
        if (spawnPrefab == null) return;

        if(canSpawn == false)
        {
            Debug.Log("canSpawn = false");
            return;
        }

        Debug.Log("SpawnItems()");

        for (int i = 0; i < spawnCount; i++)
        {
            bool valid = false;
            while (valid == false)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius));
                float dist = Vector3.Distance(spawnPos, Vector3.zero);
                if (dist <= spawnRadius && dist >= minRadius)
                {
                    valid = true;
                    Transform collectable = Instantiate(spawnPrefab, transform);
                    collectable.localPosition = spawnPos;
                    collectable.SetParent(null);
                    collectable.position = new Vector3(collectable.position.x, 1f, collectable.position.z);
                }
            }
        }

        canSpawn = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }
}
