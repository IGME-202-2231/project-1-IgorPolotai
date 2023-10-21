using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Vector2 rectSize = Vector2.one;

    bool hasAShield = false;

    public bool HasAShield
    {
        get { return hasAShield; }
        set { hasAShield = value; }
    }

    public Vector2 RectMin
    {
        get { return (Vector2)transform.position - (rectSize / 2); }
    }

    public Vector2 RectMax
    {
        get { return (Vector2)transform.position + (rectSize / 2); }
    }

    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        rectSize = spriteRenderer.bounds.extents * 2;
    }
}
