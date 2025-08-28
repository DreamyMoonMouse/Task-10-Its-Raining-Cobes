using UnityEngine;
using System.Collections;

public class BombSpawner : Spawner<Bomb>
{
    protected override void Start()
    {
        ObjectNameValue = "Бомбы";
        base.Start();
    }
    
    public void SpawnAt(Vector3 position)
    {
        Bomb bomb = Pool.GetObject();
        bomb.ResetState();
        bomb.transform.position = position;
        bomb.Initialize(MinLifetime, MaxLifetime);
        bomb.StartFadeAndExplode();
        TotalSpawnedCountValue++;
    }

    protected override void Created(Bomb bomb)
    {
        bomb.ExplosionFinished += OnBombExplosionFinished;
    }
    
    protected override IEnumerator SpawnRoutine()
    {
        yield break;
    }
    
    private void OnBombExplosionFinished(Bomb bomb)
    {
        bomb.ExplosionFinished -= OnBombExplosionFinished;

        Pool.ReturnObject(bomb);
    }
}
