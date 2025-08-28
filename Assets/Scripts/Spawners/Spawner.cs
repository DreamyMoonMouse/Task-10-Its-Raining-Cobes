using UnityEngine;
using System.Collections;

public class Spawner<T> : MonoBehaviour, ISpawnerStats where T : Component
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected Transform SpawnArea;
    [SerializeField] protected int InitialPoolSize = 10;
    [SerializeField] protected float SpawnInterval = 1f;
    [SerializeField] protected SpawnerStatsUI StatsUI; 
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
    public event System.Action StatsChanged;

    protected virtual void Start()
     {
         Pool = new Pool<T>(Prefab, InitialPoolSize, Created);
         StatsUI.RegisterSpawner(this);
     }
    
    protected virtual void Created(T instance)
    {
    }

    protected virtual IEnumerator SpawnRoutine()
    {
        yield break; 
    }
     
    protected virtual Vector3 GetRandomPosition()
     {
        if (SpawnArea.TryGetComponent(out BoxCollider box))
        {
            Vector3 min = box.bounds.min;
            Vector3 max = box.bounds.max;
            return new Vector3(
                Random.Range(min.x, max.x),
                max.y + SpawnHeightAboveArea,
                Random.Range(min.z, max.z)
            );
        }
        
        return Vector3.zero;
     }
    
    protected void NotifyStatsChanged()
    {
        StatsChanged?.Invoke();
    }
}