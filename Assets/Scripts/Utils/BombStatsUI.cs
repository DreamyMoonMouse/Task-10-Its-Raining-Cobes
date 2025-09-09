using System.Text;

public class BombStatsUI : SpawnerStatsUI
{
    protected override void AppendSpawnerStats(StringBuilder builder, ISpawnerStats spawner)
    {
        builder.AppendLine($"<b>Бомбы</b>");
        builder.AppendLine($"Заспавновано всего: {spawner.TotalSpawnedCount}");
        builder.AppendLine($"Создано новых бомб: {spawner.CreatedCount}");
        builder.AppendLine($"Активных бомб: {spawner.ActiveCount}");
        builder.AppendLine();
    }
}
