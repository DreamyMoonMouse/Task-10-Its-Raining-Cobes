using UnityEditor;

[CustomEditor(typeof(BombSpawner))]
[CanEditMultipleObjects]
public class BombSpawnerEditor : Editor
{
    private static readonly string[] _toExclude = new[] {
        "_spawnInterval",
        "_spawnHeightAboveArea",
        "_spawnArea"
    };

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, _toExclude);
        serializedObject.ApplyModifiedProperties();
    }
}