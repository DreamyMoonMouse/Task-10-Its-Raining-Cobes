using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private bool _hasCollided;
    private Renderer _cubeRenderer;
    private Color _defaultColor;
    private float _minLifetime;
    private float _maxLifetime ;
    private float _currentLifetime;
    
    public event Action<Cube> LifeEnded;
    public event Action<Vector3> BombRequested; 

    private void Awake()
    {
        _cubeRenderer = GetComponent<Renderer>();  
    }
    
    private void Start()
    {
        _defaultColor = _cubeRenderer.sharedMaterial.color; 
    }
    
    public void Initialize(float minLifetime, float maxLifetime)
    {
        _minLifetime = minLifetime;
        _maxLifetime = maxLifetime;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == false && collision.gameObject.GetComponent<PlatformMarker>() != null )
        {
            _hasCollided = true;
            ChangeColor();
            _currentLifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
            StartCoroutine(StartLifeCountdown());
        }
    }

    private void ChangeColor()
    {
        _cubeRenderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private IEnumerator StartLifeCountdown()
    {
        yield return new WaitForSeconds(_currentLifetime);
        ResetCube();
    }

    private void ResetCube()
    {
        _cubeRenderer.material.color = _defaultColor;
        _hasCollided = false;
        BombRequested?.Invoke(transform.position);
        LifeEnded?.Invoke(this);
    }
}