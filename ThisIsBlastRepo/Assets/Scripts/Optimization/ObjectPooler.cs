using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    Projectile,
    ShotParticle
}

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public PoolObjectType type;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private readonly Dictionary<PoolObjectType, Queue<GameObject>> poolDictionary = new();

    public static ObjectPooler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var pool in pools)
        {
            var queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDictionary[pool.type] = queue;
        }
    }

    public GameObject SpawnFromPool(PoolObjectType type, Vector3 position, Quaternion rotation=default, float lifetime=0f)
    {
        if (!poolDictionary.TryGetValue(type, out var queue))
        {
            Debug.LogError($"No pool found for type {type}");
            return null;
        }

        var obj = queue.Count > 0 ? queue.Dequeue() : Instantiate(GetPoolPrefab(type), transform);
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        if(lifetime > 0f)
        {
            StartCoroutine(ReturnToPoolCoroutine(type, obj, lifetime));
        }

        return obj;
    }

    public void ReturnToPool(PoolObjectType type, GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        if (obj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (poolDictionary.ContainsKey(type))
            poolDictionary[type].Enqueue(obj);
        else
            Destroy(obj);
    }

    private IEnumerator ReturnToPoolCoroutine(PoolObjectType type, GameObject obj, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool(type, obj);
    }

    private GameObject GetPoolPrefab(PoolObjectType type)
    {
        foreach (var pool in pools)
            if (pool.type == type)
                return pool.prefab;
        return null;
    }
}
