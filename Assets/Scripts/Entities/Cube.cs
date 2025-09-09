using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private bool _hasCollided;
    private float _minLifetime;
    private float _maxLifetime ;
    private float _currentLifetime;
    private ColorChanger _colorChanger;
    
    public event Action<Cube> LifeEnded;
    public event Action<Vector3> BombRequested; 

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>(); 
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
            _colorChanger.SetRandomColor();
            _currentLifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
            StartCoroutine(StartLifeCountdown());
        }
    }

    private IEnumerator StartLifeCountdown()
    {
        yield return new WaitForSeconds(_currentLifetime);
        ResetCube();
    }

    private void ResetCube()
    {
        _colorChanger.ResetColor();
        _hasCollided = false;
        BombRequested?.Invoke(transform.position);
        LifeEnded?.Invoke(this);
    }
}