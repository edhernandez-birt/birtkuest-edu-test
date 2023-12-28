using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilAlcanzaEnemigo : MonoBehaviour
{
    private GameManager gestorJuego;

    // Start is called before the first frame update
    void Start()
    {
        gestorJuego = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Colisión con enemigos y proyectiles.Destruye el enemigo y el proyectil.
    // Da puntuaciones diversas
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Se ha producido un trigger de " + this.name + " con " + other.gameObject.name);

        // Impacto con resto de enemigos
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("IMPACTO ENTRE: " + other.gameObject.name +
               this.gameObject.name);
            Destroy(other.gameObject);
            //Dan puntosBase x 2 el resto de enemigos
            if (this.name == "1projectile")
                gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase * 2, 1);
            else if (this.name == "2projectile")
            {
                gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase * 2, 2);
            }
            gestorJuego.UpdatePlayerType();
        }

        //Impacto entre proyectiles
        else if (other.gameObject.CompareTag("Bullet"))
        {
            //Dan 1/5 de los puntosBase los proyectiles destruidos
            if (this.name == "1projectile")
                gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase / 5, 1);
            else if (this.name == "2projectile")
            {
                gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase / 5, 2);
            }
            //   gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase / 5);
            Destroy(other.gameObject);
        }

        //Proyectil fallido
        else if (other.name != "Player")
        {
            //Quitan 10% de los puntos base los disparos fallados
            if (this.name == "1projectile")
                gestorJuego.ActualizarContadorPuntuacion(-gestorJuego.puntosBase / 10, 1);
            else if (this.name == "2projectile")
            {
                gestorJuego.ActualizarContadorPuntuacion(-gestorJuego.puntosBase / 10, 2);
            }

        }
        //Siempre destruimos el proyectil
        Destroy(this.gameObject);
    }
}
