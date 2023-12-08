using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bow : NetworkBehaviour
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


        if(target == null || Vector3.Distance(target.position, transform.position) > maxRange)
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
        if(nextProjectile > shootingPeriod)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);

            float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            ang = (ang + 360) % 360;// el ang va de 0 a 360 contrareloj
            //Debug.Log("ang=" + ang);

            SpriteRenderer sr = newProjectile.GetComponent<SpriteRenderer>();

            //Debug.Log("x=" + sr.bounds.size.x + ", " + "y=" + sr.bounds.size.y);
            newProjectile.transform.Rotate(0.0f, 0.0f, ang);
            float x = sr.bounds.size.x;
            float y = sr.bounds.size.y;
            //Debug.Log("x=" + x + ", " + "y=" + y);

            Collider2D col = newProjectile.GetComponent<Collider2D>();
            //porcentaje para ajustar la altura a la sombra
            //los impactos se producen a la altura del pecho
            float hp = 0.6f; 
            float ox = hp;
            float oy = hp;
            // en el editor se ha puesto el collider arriba del todo
            // al girarlo se proyectar al suelo, como si fuera una sombra
            // dependiendo del cuadrante el signo cambia para modificar el offset
            // 0-90: -x,-y  90-180: -x, +y   180-270: +x, +y   270-360: +x, -y
            if(ang > 0 && ang < 180) { ox = -1 * hp; }
            if (ang < 90 || ang > 270) { oy = -1 * hp; }
            col.offset = new Vector2(ox * x, oy * y);

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

}
