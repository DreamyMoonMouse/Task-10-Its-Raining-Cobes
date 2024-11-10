using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private bool hasCollided = false;
    private Renderer cubeRenderer;
    private Color defaultColor;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        defaultColor = cubeRenderer.sharedMaterial.color;
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("TiltedPlatform")) && !hasCollided)
        {
            hasCollided = true;
            ChangeColor();
            StartCoroutine(StartLifeCountdown());
        }
    }

    void ChangeColor()
    {
        cubeRenderer.material.color = Random.ColorHSV();
    }

    IEnumerator StartLifeCountdown()
    {
        float lifetime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(lifetime);
        ResetCube();
    }

    void ResetCube()
    {
        cubeRenderer.material.color = defaultColor;
        hasCollided = false;
        gameObject.SetActive(false);
    }
}