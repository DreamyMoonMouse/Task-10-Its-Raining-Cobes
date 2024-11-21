using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> OnLifeEnded;
    
    private bool _hasCollided;
    private Renderer _cubeRenderer;
    private Color _defaultColor;
    
    private void Awake()
    {
      _cubeRenderer = GetComponent<Renderer>();  
    }
    
    private void Start()
    {
       _defaultColor = _cubeRenderer.sharedMaterial.color; 
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == false && collision.gameObject.GetComponent<PlatformMarker>() != null )
        {
            _hasCollided = true;
            ChangeColor();
            StartCoroutine(StartLifeCountdown());
        }
    }

    private void ChangeColor()
    {
        _cubeRenderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private IEnumerator StartLifeCountdown()
    {
        float lifetime = UnityEngine.Random.Range(2f, 5f);
        
        yield return new WaitForSeconds(lifetime);
        ResetCube();
    }

    private void ResetCube()
    {
        _cubeRenderer.material.color = _defaultColor;
        _hasCollided = false;
        OnLifeEnded?.Invoke(this);
    }
}