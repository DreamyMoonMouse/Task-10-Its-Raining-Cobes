using UnityEngine;
using System.Collections;

public class BombSpawner : Spawner<Bomb>
{
    protected override void Start()
    {
        _objectName = "Бомбы";
        base.Start();
    }
    
    public void SpawnAt(Vector3 position)
    {
        Bomb bomb = _pool.GetObject();
        bomb.transform.position = position;
        bomb.Initialize(_minLifetime, _maxLifetime);
        bomb.StartFadeAndExplode();
        _totalSpawnedCount++;
    }

    protected override void Created(Bomb bomb)
    {
        bomb.ExplosionFinished += () => ReturnToPool(bomb);
    }
    
    protected override IEnumerator SpawnRoutine()
    {
        yield break;
    }
    
    private void ReturnToPool(Bomb bomb)
    {
        bomb.ExplosionFinished -= () => ReturnToPool(bomb);
        _pool.ReturnObject(bomb);
    }
}
