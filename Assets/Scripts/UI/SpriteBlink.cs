using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private float flashAlpha = 0.2f;

    private SpriteRenderer spriteRenderer;
    private float originalAlpha;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color currColor = spriteRenderer.color;
        originalAlpha = currColor.a;
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            Color currColor = spriteRenderer.color;
            currColor.a = flashAlpha;
            spriteRenderer.color = currColor;
            yield return new WaitForSeconds(flashDuration);
            currColor.a = originalAlpha;
            spriteRenderer.color = currColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
