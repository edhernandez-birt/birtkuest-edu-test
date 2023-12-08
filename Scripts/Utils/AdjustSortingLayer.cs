using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSortingLayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D colliderCapa;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderCapa = GetComponent<Collider2D>();
    }

    void Update()
    {
        float y = transform.position.y;
        //float h = spriteRenderer.bounds.size.y;
        ////float h = spriteRenderer.sprite.rect.y;//bad
        //int layer = Mathf.RoundToInt((y - h / 2) * 100f) * -1; 
        //Debug.Log(this.name + "\t" + y + "\t" + (-h/2)+ "\t" + layer);
        ////spriteRenderer.sortingOrder = layer;
        // Parece mejor usar el offset del collider por las piedras.

        if (colliderCapa != null) //A veces intenta acceder y ya no existe el collider...
        {
        float c = colliderCapa.offset.y;
        int layer = Mathf.RoundToInt((y + c) * 100f) * -1;
        //Debug.Log(this.name + "\t" + y + "\t" + c + "\t" + layer);
        spriteRenderer.sortingOrder = layer;
        }
    }
}
