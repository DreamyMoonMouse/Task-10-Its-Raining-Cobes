using UnityEngine;
using System;

public class Spawner<T> : MonoBehaviour, ISpawnerStats where T : Component
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected Transform SpawnArea;
    [SerializeField] protected int InitialPoolSize = 10;
    [SerializeField] protected float SpawnInterval = 1f;
    [SerializeField] protected float SpawnHeightAboveArea = 5f;
    [SerializeField] protected float MinLifetime = 2f;
    [SerializeField] protected float MaxLifetime = 5f;

    protected string ObjectNameValue = "Объект";
    protected Pool<T> Pool;
    protected int TotalSpawnedCountValue;

    public int TotalSpawnedCount => TotalSpawnedCountValue;
    public int CreatedCount => Pool != null ? Pool.TotalCreatedCount : 0; 
    public int ActiveCount => Pool?.ActiveCount ?? 0;
    public string ObjectName => ObjectNameValue;
    public event Action Spawned;
    public event Action Created;
    public event Action Returned;

    protected virtual void Start()
     {
         Pool = new Pool<T>(Prefab, InitialPoolSize, OnCreated);
     }
    
    protected virtual void OnCreated(T instance)
    {
        Created?.Invoke();
    }

    protected void OnSpawned()
    {
        Spawned?.Invoke();
    }

    protected void OnReturned()
    {
        Returned?.Invoke();
    }
     
    protected virtual Vector3 GetRandomPosition()
     {
        if (SpawnArea.TryGetComponent(out BoxCollider box))
        {
            Vector3 min = box.bounds.min;
            Vector3 max = box.bounds.max;
            return new Vector3(
                UnityEngine.Random.Range(min.x, max.x),
                max.y + SpawnHeightAboveArea,
                UnityEngine.Random.Range(min.z, max.z)
            );
        }
        
        return Vector3.zero;
     }
}