using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaunchBoomerang : MonoBehaviour
{
    public GameObject proyectil = null;
    public float tiempo = 5.0f;
    private float siguienteProyectil = 0f;
    public float proyectilSpeed = 5f;
    private Animator myAnim;
    private Transform player;
 //   private bool animamacionLanzamiento = false;

    void Start()
    {
        siguienteProyectil = 0f;
        player = FindObjectOfType<PlayerController>().transform;

        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        siguienteProyectil += Time.deltaTime;
        //Medio segundo antes del lanzamiento inicia la animación de lanzamiento
        if (siguienteProyectil > (tiempo - 0.5f) && !myAnim.GetBool("isLaunching"))
        {
         //   animamacionLanzamiento = true;
            myAnim.SetBool("isLaunching", true);
            myAnim.SetFloat("moveX", (player.position.x - transform.position.x));
            myAnim.SetFloat("moveY", (player.position.y - transform.position.y));
           // playerAnimator.SetFloat("AimMagnitude", moveInput.magnitude);
           // playerAnimator.SetBool("Aim", Input.GetButton("Fire1"));
        }
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
                   
                    newProjectile.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

                }

                Object.Destroy(newProjectile,4.5f);
            }

            //  disparar();
            siguienteProyectil = 0f;
           // animamacionLanzamiento = false;
            myAnim.SetBool("isLaunching", false);

        }
    }

    void disparar()
    {
        GameObject projectileGameObject = Instantiate(proyectil, this.transform.position, transform.rotation, null);
    }
}
