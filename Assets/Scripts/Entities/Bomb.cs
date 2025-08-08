using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Renderer), typeof(Rigidbody), typeof(SphereCollider))]
public class Bomb : MonoBehaviour
{
    private float _minLifetime;
    private float _maxLifetime ;
    private float _currentLifetime;
    private float _explosionRadius = 3f;
    private Material _materialInstance;
    private Color _startColor; 
    private Coroutine _fadeRoutine;
    
    public event Action<Bomb> ExplosionFinished;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        _materialInstance = renderer.material; 
        _startColor = Color.black;
        _startColor.a = 1.0f; 
        _materialInstance.color = _startColor;
        _currentLifetime = 0f;
    }
    
    public void Initialize(float minDuration, float maxDuration)
    {
        _minLifetime = minDuration;
        _maxLifetime = maxDuration; 
    }

    public void StartFadeAndExplode()
    {
        _currentLifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
        
        if (_fadeRoutine != null)
            StopCoroutine(_fadeRoutine);
        
        _fadeRoutine = StartCoroutine(FadeAndExplode());
    }
    
    public void ResetState()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
            _fadeRoutine = null;
        }
        
        _currentLifetime = 0f;
        
        if (_materialInstance != null && _startColor != null)
        {
            Color color = _materialInstance.color;
            color.a = 1f;
            _materialInstance.color = color;
            SetMaterialToFadeMode();
        }
    }
    
    private void SetMaterialToFadeMode()
    {
        _materialInstance.SetFloat("_Mode", 2); 
        _materialInstance.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _materialInstance.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _materialInstance.SetInt("_ZWrite", 0);
        _materialInstance.DisableKeyword("_ALPHATEST_ON");
        _materialInstance.EnableKeyword("_ALPHABLEND_ON");
        _materialInstance.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _materialInstance.renderQueue = 3000;
    }

    private IEnumerator FadeAndExplode()
    {
        float elapsed = 0f;
        Color currentColor = _startColor; 

        while (elapsed < _currentLifetime)
        {
            float t = elapsed / _currentLifetime;
            currentColor.a = Mathf.Lerp(1f, 0f, t);
            _materialInstance.color = currentColor;
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        currentColor.a = 0f;
        _materialInstance.color = currentColor;

        Explode();
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);
        
        foreach (var hit in hits)
        {
            if (hit.attachedRigidbody != null && hit.attachedRigidbody != GetComponent<Rigidbody>())
                hit.attachedRigidbody.AddExplosionForce(300f, transform.position, _explosionRadius);
        }
        
        ExplosionFinished?.Invoke(this);
    }
    
    private void OnDisable()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
            _fadeRoutine = null;
        }
    }
}