using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaunchFireball : MonoBehaviour
{
    public GameObject proyectil = null;
    public float tiempo = 5.0f;
    private float siguienteProyectil = 0f;
    public float proyectilSpeed = 5f;


    private Transform player;

    void Start()
    {
        siguienteProyectil = 0f;
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        siguienteProyectil += Time.deltaTime;
        if (siguienteProyectil > tiempo)
        {
            if (player != null)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                GameObject newProjectile = Instantiate(proyectil, transform.position, Quaternion.identity);
                Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.velocity = direction * proyectilSpeed;
                }
              
                Object.Destroy(newProjectile,4.5f);
            }

            //  disparar();
            siguienteProyectil = 0f;
        }
    }

    void disparar()
    {
        GameObject projectileGameObject = Instantiate(proyectil, this.transform.position, transform.rotation, null);
    }
}
