using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Bounty : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected int points = 10;

    protected bool alreadyCollected;

    public abstract int Collect();

    public void SetValue(int value)
    {
        points = value;
    }

    protected void Show(bool show)
    {
        spriteRenderer.enabled = show;
    }

}
