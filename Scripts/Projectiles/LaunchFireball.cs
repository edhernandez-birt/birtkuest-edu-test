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
      //  player = FindObjectOfType<PlayerControllerGirl>().transform;
    }

    void Update()
    {
        siguienteProyectil += Time.deltaTime;
        
    }
    private void FixedUpdate()
    {
        player = getClosestPlayer();
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

                Object.Destroy(newProjectile, tiempo-0.5f);
            }
            siguienteProyectil = 0f;
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
