using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.sharedMaterial.color;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    public void ResetColor()
    {
        _renderer.material.color = _defaultColor;
    }
}
