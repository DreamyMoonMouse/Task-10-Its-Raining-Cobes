using UnityEngine;
using System.Collections.Generic;
using System.Text;

public abstract class SpawnerStatsUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _statsText;
    
    private List<ISpawnerStats> _spawners = new();

    public void RegisterSpawner(ISpawnerStats spawner)
    {
        if (spawner == null) return;

        _spawners.Add(spawner);
        spawner.Spawned += UpdateStats;
        spawner.Created += UpdateStats;
        spawner.Returned += UpdateStats;
        UpdateStats();
    }
    
    protected abstract void AppendSpawnerStats(StringBuilder builder, ISpawnerStats spawner);
    
    private void OnDestroy()
    {
        foreach (var spawner in _spawners)
        {
            spawner.Spawned -= UpdateStats;
            spawner.Created -= UpdateStats;
            spawner.Returned -= UpdateStats;
        }
    }

    private void UpdateStats()
    {
        var builder = new StringBuilder();

        foreach (var spawner in _spawners)
        {
            AppendSpawnerStats(builder, spawner);
        }

        _statsText.text = builder.ToString();
    }
}
