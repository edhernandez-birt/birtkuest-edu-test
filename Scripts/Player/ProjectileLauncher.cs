using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileLauncher : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject serverProjectilePrefab;
    [SerializeField] private GameObject clientProjectilePrefab;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Animator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float firingTime = 0.3f;

    private PlayerController playerController;

    private bool shouldFire = false;
    private float previousFireTime = 0f;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner){ return; }

        inputReader.PrimaryFireEvent += HandlePrimaryFire;

        playerController = GetComponent<PlayerController>();

        previousFireTime = Time.time;
    }

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) { return; }

        inputReader.PrimaryFireEvent -= HandlePrimaryFire;
    }

    private void HandlePrimaryFire(bool shouldFire)
    {
        this.shouldFire = shouldFire;
    }

    private void Update()
    {
        if(!IsOwner) { return; }

        if ((Time.time - previousFireTime > firingTime) && playerAnimator.GetBool("isLaunching"))
        {
            playerAnimator.SetBool("isLaunching", false);

            Vector2 direction = playerController.LastDirection;

            PrimaryFireServerRpc(transform.position, direction);

            SpawnDummyProjectile(transform.position, direction);
        }

        if (!shouldFire) { return; }

        if ((Time.time - previousFireTime >= 1 / fireRate) && !playerAnimator.GetBool("isLaunching"))
        {
            previousFireTime = Time.time;
            playerAnimator.SetBool("isLaunching", true);
        }

    }
        

    [ServerRpc]
    private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
    {
        //Debug.Log("PrimaryFireServerRpc 0");
        // Cuando está pegado a una pared si dispara se choca la piedra, por eso la instancio separada
        // Si la dirección es hacia arriba, no lo modifico porque dispararía a la pared la posición de pintado importa
        if(direction != Vector3.up) { spawnPos.y = spawnPos.y - 0.1f; }

        GameObject projectileInstance = Instantiate(
            serverProjectilePrefab, 
            spawnPos, 
            Quaternion.identity);

        Physics2D.IgnoreCollision(playerCollider, projectileInstance.GetComponent<Collider2D>());

        if(projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = direction * projectileSpeed;
        }

        SpawnDummyProjectileClientRpc(spawnPos, direction);

    }

    [ClientRpc]
    private void SpawnDummyProjectileClientRpc(Vector3 spawnPos, Vector3 direction)
    {
        if (IsOwner) { return; }

        SpawnDummyProjectile(spawnPos, direction);        
    }

    private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
        //Debug.Log("SpawnDummyProjectile 0");
        // Cuando está pegado a una pared si dispara se choca la piedra, por eso la instancio separada
        // Si la dirección es hacia arriba, no lo modifico porque dispararía a la pared la posición de pintado importa
        if(direction != Vector3.up) { spawnPos.y = spawnPos.y - 0.1f; }

        GameObject projectileInstance = Instantiate(
            clientProjectilePrefab, 
            spawnPos, 
            Quaternion.identity);

        Physics2D.IgnoreCollision(playerCollider, projectileInstance.GetComponent<Collider2D>());

        if (projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = direction * projectileSpeed;
        }

    }


}
