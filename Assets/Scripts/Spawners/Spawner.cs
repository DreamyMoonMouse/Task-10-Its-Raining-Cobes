using UnityEngine;
using System.Collections;

public class Spawner<T> : MonoBehaviour, ISpawnerStats where T : Component
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected Transform _spawnArea;
    [SerializeField] protected int _initialPoolSize = 10;
    [SerializeField] protected float _spawnInterval = 1f;
    [SerializeField] protected SpawnerStatsUI _statsUI; 
    [SerializeField] protected float _spawnHeightAboveArea = 5f;
    [SerializeField] protected float _minLifetime = 2f;
    [SerializeField] protected float _maxLifetime = 5f;

    protected string _objectName = "Объект";
    protected Pool<T> _pool;
    protected int _totalSpawnedCount;

    public int TotalSpawnedCount => _totalSpawnedCount;
    public int CreatedCount => _pool != null ? _pool.TotalCreatedCount : 0; 
    public int ActiveCount => _pool?.ActiveCount ?? 0;
    public string ObjectName => _objectName;

    protected virtual void Start()
     {
         _pool = new Pool<T>(_prefab, _initialPoolSize, Created);
         _statsUI.RegisterSpawner(this);
         StartCoroutine(SpawnRoutine());
     }
    
    protected virtual void Created(T instance)
    {
    }

    protected virtual IEnumerator SpawnRoutine()
    {
        bool isRunning = true;
         
        while (isRunning)
        {
             var obj = _pool.GetObject();
             obj.transform.position = GetRandomPosition();
             _totalSpawnedCount++;
             
             yield return new WaitForSeconds(_spawnInterval);
        }
    }
     
     private Vector3 GetRandomPosition()
     {
        if (_spawnArea.TryGetComponent(out BoxCollider box))
        {
            Vector3 min = box.bounds.min;
            Vector3 max = box.bounds.max;
            return new Vector3(
                Random.Range(min.x, max.x),
                max.y + _spawnHeightAboveArea,
                Random.Range(min.z, max.z)
            );
        }
        
        return Vector3.zero;
     }
}