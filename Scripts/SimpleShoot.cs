using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShoot : MonoBehaviour
{
    public GameObject proyectil = null;
    public float tiempo = 5.0f;
    private float siguienteProyectil = 0f;

    void Start()
    {
        siguienteProyectil = 0f;
    }

    void Update()
    {
        siguienteProyectil += Time.deltaTime;
        if (siguienteProyectil > tiempo)
        {
            disparar();
            siguienteProyectil = 0f;
        }
    }

    void disparar()
    {
        GameObject projectileGameObject = Instantiate(proyectil, this.transform.position, transform.rotation, null);
    }
}
