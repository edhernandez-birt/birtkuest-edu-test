using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaunchBoomerang : MonoBehaviour
{
    [SerializeField] private GameObject proyectil = null;
    [SerializeField] private float tiempo = 5.0f;
    [SerializeField] private float proyectilSpeed = 5f;
    private float siguienteProyectil = 0f;
    private Animator myAnim;
    private Transform player;

    void Start()
    {
        siguienteProyectil = 0f;
        //Con dos players usamos getClosestPlayer
        //player = FindObjectOfType<PlayerControllerGirl>().transform;
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        siguienteProyectil += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        player = getClosestPlayer();
        if (player != null)
        {
            //Medio segundo antes del lanzamiento inicia la animaci�n de lanzamiento
            if (siguienteProyectil > (tiempo - 0.5f) && !myAnim.GetBool("isLaunching"))
            {
                myAnim.SetBool("isLaunching", true);
                myAnim.SetFloat("moveX", (player.position.x - transform.position.x));
                myAnim.SetFloat("moveY", (player.position.y - transform.position.y));
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
                        // Rotaci�n sobre su propio eje se podr�a hacer f�cilmente?
                        //newProjectile.transform.Rotate(new Vector3(90f, 90f, 0f) * Time.deltaTime);
                    }

                    Object.Destroy(newProjectile, tiempo - 0.5f);
                }
                //Contador a 0 para volver a lanzar otro proyectil
                siguienteProyectil = 0f;
                myAnim.SetBool("isLaunching", false);
            }
        }
    }

    private Transform getClosestPlayer()
    {
        PlayerControllerGirl[] targets = FindObjectsOfType<PlayerControllerGirl>();
        if (targets.Length == 0) { return null; }
        if (targets.Length == 1) { return targets[0].transform; }
        int idx = 0;
        double distance = double.MaxValue;
        for (int i = 0; i < targets.Length; i++)
        {
            double di = Vector3.Distance(transform.position, targets[i].transform.position);
            if (di < distance)
            {
                distance = di;
                idx = i;
            }
        }
        return targets[idx].transform;
    }

}
