using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyController : NetworkBehaviour
{

    [Header("References")]
    [SerializeField] private Animator myAnimator;
    [SerializeField] private Health myHealth;
    [SerializeField] private Transform homePosition;   

    [Header("Settings")]
    [SerializeField] private float speed = 1.2f;
    [SerializeField] private float maxRange = 6f;
    [SerializeField] private float minRange = 1f;
    [SerializeField] private float fadeOutTimeOnDie = 10f;

    private Transform target;


    public override void OnNetworkSpawn()
    {
        myHealth.OnDie += HandleOnDie;
    }

    public override void OnNetworkDespawn()
    {
        myHealth.OnDie -= HandleOnDie;
    }

    private void HandleOnDie(Health health)
    {
        //Debug.Log("HandleOnDie IsServer=" + IsServer + " IsHost=" + IsHost + " IsClient=" + IsClient + " IsOwner=" + IsOwner);
        Die();
        //Debug.Log("HandleOnDie 1");
        DieClientRpc();
        //Debug.Log("HandleOnDie 2");
    }


    [ClientRpc]
    private void DieClientRpc()
    {
        //Debug.Log("DieClientRpc IsServer=" + IsServer + " IsHost=" + IsHost + " IsClient=" + IsClient + " IsOwner=" + IsOwner);
        if(IsOwner) { return; }

        //Debug.Log("DieClientRpc 1");
        Die();

        //Debug.Log("DieClientRpc 2");
    }

    private void Die()
    {
        //Debug.Log("Die IsServer=" + IsServer + " IsHost=" + IsHost + " IsClient=" + IsClient + " IsOwner=" + IsOwner);
        if (TryGetComponent<Slingshot>(out Slingshot slingshot))
        {
            Destroy(slingshot);
        }
        if (TryGetComponent<Bow>(out Bow bow))
        {
            Destroy(bow);
        }
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = Vector2.zero;
        }
        if (TryGetComponent<AdjustSortingLayer>(out AdjustSortingLayer asl))
        {
            Destroy(asl);
        }
        if (TryGetComponent<Collider2D>(out Collider2D collider))
        {
            Destroy(collider);
        }
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.sortingOrder = 1000;
            spriteRenderer.sortingLayerName = "Default";
            StartCoroutine(FadeTo(spriteRenderer, 0.0f, fadeOutTimeOnDie));
        }
        myAnimator.SetBool("isHurt", true);
        Destroy(gameObject, fadeOutTimeOnDie);
    }

    IEnumerator FadeTo(SpriteRenderer spriteRenderer, float aValue, float aTime)
    {
        if (TryGetComponent<Transform>(out Transform transform))
        {
            float alpha = spriteRenderer.color.a;
            for(float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
                spriteRenderer.material.color = newColor;
                yield return null;
            }
        }
    }

    void Update()
    {
        target = getClosestPlayer();

        if (target!=null && !myAnimator.GetBool(name: "isLaunching") && !myAnimator.GetBool(name: "isHurt"))
        {
            Vector3 direction = (new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0)).normalized;
            myAnimator.SetFloat("aimX", direction.x);
            myAnimator.SetFloat("aimY", direction.y);

            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
            {
                FollowPlayer();
            } else if (Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                GoHome();
            }

        }

    }

    private Transform getClosestPlayer()
    {
        PlayerController[] targets = FindObjectsOfType<PlayerController>();
        if(targets.Length == 0) { return null; }
        if(targets.Length == 1) { return targets[0].transform; }
        int idx = 0;
        double distance = double.MaxValue;
        for(int i = 0; i < targets.Length; i++)
        {
            double di = Vector3.Distance(transform.position, targets[i].transform.position);
            if(di < distance)
            {
                distance = di;
                idx = i;
            }
        }
        return targets[idx].transform;
    }

    public void FollowPlayer()
    {
        myAnimator.SetBool("isMoving", true);
        Vector3 direction = (new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0)).normalized;
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void GoHome()
    {
        if (Vector3.Distance(transform.position, homePosition.position) > 0.1)
        {
            myAnimator.SetBool("isMoving", true);
            Vector3 direction = (new Vector3(homePosition.position.x - transform.position.x, homePosition.position.y - transform.position.y, 0)).normalized;
            myAnimator.SetFloat("moveX", direction.x);
            myAnimator.SetFloat("moveY", direction.y);
            transform.position = Vector3.MoveTowards(transform.position, homePosition.transform.position, speed * Time.deltaTime);
        } 
        else
        {
            myAnimator.SetBool("isMoving", false);
        }
    }
}
    