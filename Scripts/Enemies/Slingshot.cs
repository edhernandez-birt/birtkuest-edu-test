using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Slingshot : NetworkBehaviour
{
    [Header("References")]
    public Animator myAnimator;
    public GameObject projectile = null;
    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private float shootingPeriod = 5.0f;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float firingTime = 0.6f;
    [SerializeField] private float maxRange = 6f;

    private Transform target;

    private float nextProjectile = 0f;

    void Update()
    {
        target = getClosestPlayer();


        if (target == null || Vector3.Distance(target.position, transform.position) > maxRange)
        {
            nextProjectile = 0f;
            myAnimator.SetBool("isLaunching", false);
            return;
        }


        nextProjectile += Time.deltaTime;
        if(nextProjectile > (shootingPeriod - firingTime) && !myAnimator.GetBool("isLaunching"))
        {
            myAnimator.SetBool("isLaunching", true);
        }
        if (nextProjectile > shootingPeriod)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

            Physics2D.IgnoreCollision(enemyCollider, newProjectile.GetComponent<Collider2D>());// Puede dar error, cuidado en la definición de las capas

            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

            rb.velocity = direction * projectileSpeed;
                
            Destroy(newProjectile, (shootingPeriod - firingTime));


            nextProjectile = 0f;

            myAnimator.SetBool("isLaunching", false);
        } 
    }

    private Transform getClosestPlayer()
    {
        PlayerController[] targets = FindObjectsOfType<PlayerController>();
        if(targets.Length == 0) { return null; }
        if(targets.Length == 1) { return targets[0].transform; }
        int idx = 0;
        double distance = double.MaxValue;
        for (int i = 0; i < targets.Length; i++)
        {
            double di = Vector3.Distance(transform.position, targets[i].transform.position);
            if ( di < distance) {
                distance = di;
                idx = i;
            }
        }
        return targets[idx].transform;
    }

}
