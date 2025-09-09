using System;

public interface ISpawnerStats
{
    int TotalSpawnedCount { get; }
    int CreatedCount { get; }
    int ActiveCount { get; }
    string ObjectName { get; }
    
    event Action Spawned;
    event Action Created;
    event Action Returned;
}
