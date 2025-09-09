using System.Text;

public class CubeStatsUI : SpawnerStatsUI
{
    protected override void AppendSpawnerStats(StringBuilder builder, ISpawnerStats spawner)
    {
        builder.AppendLine($"<b>Кубы</b>");
        builder.AppendLine($"Заспавновано всего: {spawner.TotalSpawnedCount}");
        builder.AppendLine($"Создано новых кубов: {spawner.CreatedCount}");
        builder.AppendLine($"Активных кубов: {spawner.ActiveCount}");
        builder.AppendLine();
    }
}
