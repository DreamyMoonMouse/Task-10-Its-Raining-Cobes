using System;

public interface ISpawnerStats
{
    int TotalSpawnedCount { get; }
    int CreatedCount { get; }
    int ActiveCount { get; }
    string ObjectName { get; }
    event Action StatsChanged;
}
