using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Collections;

public class SpawnerStatsUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _statsText;
    [SerializeField] private float _updateInterval = 0.5f;
    
    private List<ISpawnerStats> _spawners = new();

    public void RegisterSpawner(ISpawnerStats spawner)
    {
        _spawners.Add(spawner);
    }

    private void Start()
    {
        StartCoroutine(UpdateStats());
    }

    private IEnumerator UpdateStats()
    {
        bool enabled = true;
        
        while (enabled)
        {
            var builder = new StringBuilder();
            
            foreach (var spawner in _spawners)
            {
                builder.AppendLine($"<b>{spawner.ObjectName}</b>");
                builder.AppendLine($"Заспавновано всего: {spawner.TotalSpawnedCount}");
                builder.AppendLine($"Создано новых объектов: {spawner.CreatedCount}");
                builder.AppendLine($"Активно объектов: {spawner.ActiveCount}");
                builder.AppendLine();
            }
            
            _statsText.text = builder.ToString();
            yield return new WaitForSeconds(_updateInterval);
        }
    }
}
