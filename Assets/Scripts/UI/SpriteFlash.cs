using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private float flashAlpha = 0.2f;

    private SpriteRenderer spriteRenderer;
    private float originalAlpha;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color currColor = spriteRenderer.color;
        originalAlpha = currColor.a;
    }

    private void OnEnable()
    {
        Health.onTakeDamage += Flash;
    }

    private void OnDisable()
    {
        Health.onTakeDamage -= Flash;
    }

    private void Flash(GameObject obj)
    {
        if (obj == gameObject)
        {
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        Color currColor = spriteRenderer.color;
        currColor.a = flashAlpha;
        spriteRenderer.color = currColor;
        yield return new WaitForSeconds(flashDuration);
        currColor.a = originalAlpha;
        spriteRenderer.color = currColor;
    }
}
