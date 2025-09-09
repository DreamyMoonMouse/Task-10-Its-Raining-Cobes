using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    
    [SerializeField] private BombStatsUI _ui;
    
    protected override void Start()
    {
        ObjectNameValue = "Бомбы";
        base.Start();
        _ui.RegisterSpawner(this);
    }
    
    public void SpawnAt(Vector3 position)
    {
        Bomb bomb = Pool.GetObject();
        bomb.ResetState();
        bomb.transform.position = position;
        bomb.Initialize(MinLifetime, MaxLifetime);
        bomb.StartFadeAndExplode();
        TotalSpawnedCountValue++;
        OnSpawned();
    }

    protected override void OnCreated(Bomb bomb)
    {
        base.OnCreated(bomb);
        bomb.ExplosionFinished += OnBombExplosionFinished;
    }
    
    private void OnBombExplosionFinished(Bomb bomb)
    {
        bomb.ExplosionFinished -= OnBombExplosionFinished;
        Pool.ReturnObject(bomb);
        OnReturned();
    }
}
