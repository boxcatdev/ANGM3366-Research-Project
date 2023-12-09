using System.Collections;
using System.Collections.Generic;
using System.Transactions;
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
    [Space]
    [SerializeField] private bool canCombine = false;

    public float cooldownProgress {  get; private set; }
    public bool canSpawn { get; private set; }

    private Damageable damageable;

    [Header("Unity Events")]
    public UnityEvent OnCooldownComplete;

    //combine collectables
    private float combinePeriod = 10f;
    private float combineProgress;
    private float combineRange = 0.5f;

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

        combineProgress = combinePeriod;
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

        #region Combine Collectables
        if(canCombine == true)
        {
            combineProgress -= Time.deltaTime;
            if (combineProgress <= 0)
            {
                combineProgress = combinePeriod;
                CombineCollectables();
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
                    //collectable.GetComponent<Collectable>().priority = i;
                    collectable.localPosition = spawnPos;
                    collectable.SetParent(null);
                    collectable.position = new Vector3(collectable.position.x, 1f, collectable.position.z);
                }
            }
        }

        canSpawn = false;
    }
    private void CombineCollectables()
    {
        Debug.Log("CombineCollectables()");
        List<Collectable> collectables = new List<Collectable>(FindObjectsOfType<Collectable>());
        int count = 0;

        //stuff
        foreach (Collectable collectable in collectables)
        {
            if(collectable != null)
            {
                Debug.LogWarning("exists");
                count++;

                foreach (var other in collectables)
                {
                    if(other != null)
                    {
                        if (Vector3.Distance(collectable.transform.position, other.transform.position) <= combineRange
                        && other.hasCombined == false)
                        {
                            collectable.value += other.value;
                            other.hasCombined = true;
                            Destroy(other);
                            Destroy(other.gameObject);
                        }
                    }
                    
                }

                Debug.Log("New Value: " + collectable.value);
                /*for (int i = 0; i < collectables.Count; i++)
                {
                    if (i > collectables.Count * 0.5f)
                    {
                        collectables[i].DestroySelf();
                    }

                }*/

            }
        }

        Debug.LogWarning("Remaining: " + count);

        //pick random and combine nearby
        /*int index = Random.Range(0, collectables.Count);

        Collectable collectable = collectables[index];
        foreach (var other in collectables)
        {
            if (Vector3.Distance(collectable.transform.position, other.transform.position) <= combineRange)
            {

            }
        }*/
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.DrawWireSphere(transform.position, minRadius);
    }
}
