using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderComponent : MonoBehaviour
{

    /// <summary>
    /// The sprite renderer to fade out
    /// </summary>
    protected SpriteRenderer _spriteRenderer;
    protected Image _image;
    protected Text _text;

    /// <summary>
    /// How long it takes to fade the sprite out.
    /// </summary>
    [Tooltip("How long it takes to fade the sprite out.")]
    public float fadeOutTimeInSeconds = 1f;

    /// <summary>
    /// Whether to destroy the object at end of fade.
    /// </summary>
    [Tooltip("Whether to destroy the object at end of fade.")]
    public bool thenDestroy = false;

    /// <summary>
    /// Whether the sprite fades out.
    /// </summary>
    [Tooltip("Whether the sprite fades out.")]
    public bool fadeOut = false;

    /// <summary>
    /// Whether the sprite fades in.
    /// </summary>
    [Tooltip("Whether the sprite fades in.")]
    public bool fadeIn = false;

    /// <summary>
    /// The direction the alpha is being modified.
    /// </summary>
    [Tooltip("The direction the alpha is currently being modified.")]
    public int modifier = -1;

    /// <summary>
    /// The minimum alpha (transparency).
    /// </summary>
    [Tooltip("The minimum alpha (transparency).")]
    public float minAlpha = 0f;

    /// <summary>
    /// The maximum alpha (transparency).
    /// </summary>
    [Tooltip("The maximum alpha (transparency).")]
    public float maxAlpha = 1f;


    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _text = GetComponent<Text>();
        _image = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if (_spriteRenderer)
        {
            float r = _spriteRenderer.color.r;
            float g = _spriteRenderer.color.g;
            float b = _spriteRenderer.color.b;
            float a = _spriteRenderer.color.a + (Time.fixedDeltaTime / fadeOutTimeInSeconds * modifier);
            _spriteRenderer.color = new Color(r, g, b, a);
            // this checks if faded out to minimal alpha
            if (modifier == -1) // Fading out
            {
                if (_spriteRenderer.color.a <= minAlpha)
                {
                    a = minAlpha;
                    _spriteRenderer.color = new Color(r, g, b, a);

                    if (thenDestroy)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (fadeIn)
                        {
                            modifier *= -1;
                        }
                    }
                }
            }
            else if (modifier == 1) // Fading in
            {
                // TODO: Fade in

                if (_spriteRenderer.color.a <= minAlpha)
                {
                    a = minAlpha;
                    _spriteRenderer.color = new Color(r, g, b, a);

                    if (thenDestroy)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (fadeOut)
                        {
                            modifier *= -1;
                        }
                    }
                }
            }
            else // modifier == 0?
            {
                // TODO: pause between fade in/out cycles?
                throw new ArgumentNullException("Modifier unset.");
            }
        }
        if (_text)
        {
            float r = _text.color.r;
            float g = _text.color.g;
            float b = _text.color.b;
            float a = _text.color.a + (Time.fixedDeltaTime / fadeOutTimeInSeconds * modifier);
            _text.color = new Color(r, g, b, a);
            // this checks if faded out to minimal alpha
            if (modifier == -1) // Fading out
            {
                if (_text.color.a <= minAlpha)
                {
                    a = minAlpha;
                    _text.color = new Color(r, g, b, a);

                    if (thenDestroy)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (fadeIn)
                        {
                            modifier *= -1;
                        }
                    }
                }
            }
            else if (modifier == 1) // Fading in
            {
                // TODO: Fade in

                if (_text.color.a <= minAlpha)
                {
                    a = minAlpha;
                    _text.color = new Color(r, g, b, a);

                    if (thenDestroy)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (fadeOut)
                        {
                            modifier *= -1;
                        }
                    }
                }
            }
            else // modifier == 0?
            {
                // TODO: pause between fade in/out cycles?
                throw new ArgumentNullException("Modifier unset.");
            }
        }
        if (_image)
        {
            float r = _image.color.r;
            float g = _image.color.g;
            float b = _image.color.b;
            float a = _image.color.a + (Time.fixedDeltaTime / fadeOutTimeInSeconds * modifier);
            _image.color = new Color(r, g, b, a);
            // this checks if faded out to minimal alpha
            if (modifier == -1) // Fading out
            {
                if (_image.color.a <= minAlpha)
                {
                    a = minAlpha;
                    _image.color = new Color(r, g, b, a);

                    if (thenDestroy)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (fadeIn)
                        {
                            modifier *= -1;
                        }
                    }
                }
            }
            else if (modifier == 1) // Fading in
            {
                // TODO: Fade in

                if (_image.color.a <= minAlpha)
                {
                    a = minAlpha;
                    _image.color = new Color(r, g, b, a);

                    if (thenDestroy)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (fadeOut)
                        {
                            modifier *= -1;
                        }
                    }
                }
            }
            else // modifier == 0?
            {
                // TODO: pause between fade in/out cycles?
                throw new ArgumentNullException("Modifier unset.");
            }
        }
    }
}
