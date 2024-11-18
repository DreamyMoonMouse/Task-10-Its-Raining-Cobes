using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private bool hasCollided;
    private Renderer cubeRenderer;
    private Color defaultColor;
    private Pool pool;
    
    private void Awake()
    {
      cubeRenderer = GetComponent<Renderer>();  
    }
    private void Start()
    {
       defaultColor = cubeRenderer.sharedMaterial.color; 
    }
    
    public void SetPool(Pool pool)
    {
        this.pool = pool;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlatformMarker>() != null && hasCollided == false)
        {
            hasCollided = true;
            ChangeColor();
            StartCoroutine(StartLifeCountdown());
        }
    }

    private void ChangeColor()
    {
        cubeRenderer.material.color = Random.ColorHSV();
    }

    IEnumerator StartLifeCountdown()
    {
        float lifetime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(lifetime);
        ResetCube();
    }

    private void ResetCube()
    {
        cubeRenderer.material.color = defaultColor;
        hasCollided = false;
        pool.ReturnObject(gameObject);
    }
}