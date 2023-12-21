using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileLauncherGirl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
   // [SerializeField] private GameObject serverProjectilePrefab;
    [SerializeField] private GameObject projectilePrefab;
     private Collider2D playerCollider;
     private Animator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float firingTime = 0.3f;

    private PlayerControllerGirl playerControllerGirl;

    private bool shouldFire = false;
    private float previousFireTime = 0f;

    private void HandlePrimaryFire(bool shouldFire)
    {
        this.shouldFire = shouldFire;
    }

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        playerControllerGirl = GetComponent<PlayerControllerGirl>();

        //Player 1 New Input Style
        if (playerControllerGirl.PlayerId == 1) { 
            inputReader.PrimaryFireEvent += HandlePrimaryFire;
        }

        previousFireTime = Time.time;
    }

    private void Update()
    {
        //Player 2 Old Input Style
        if (playerControllerGirl.PlayerId == 2)
        {
            HandlePrimaryFire(Input.GetKeyDown(KeyCode.K));
        }

        if ((Time.time - previousFireTime > firingTime) && playerAnimator.GetBool("isLaunching"))
        {
            playerAnimator.SetBool("isLaunching", false);

            Vector2 direction = playerControllerGirl.LastDirection;

            SpawnDummyProjectile(transform.position, direction);
        }

        if (!shouldFire) { return; }

        if ((Time.time - previousFireTime >= 1 / fireRate) && !playerAnimator.GetBool("isLaunching"))
        {
            previousFireTime = Time.time;
            playerAnimator.SetBool("isLaunching", true);
        }

    }
        

   

 
    private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
        //Debug.Log("SpawnDummyProjectile 0");
        // Cuando está pegado a una pared si dispara se choca la piedra, por eso la instancio separada
        // Si la dirección es hacia arriba, no lo modifico porque dispararía a la pared la posición de pintado importa
        if(direction != Vector3.up) { spawnPos.y = spawnPos.y - 0.1f; }

        GameObject projectileInstance = Instantiate(
            projectilePrefab, 
            spawnPos, 
            Quaternion.identity);

        Physics2D.IgnoreCollision(playerCollider, projectileInstance.GetComponent<Collider2D>());

        if (projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = direction * projectileSpeed;
        }

    }


}
