using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilAlcanzaEnemigo : MonoBehaviour
{
    //public int puntosBase = 100;  //Lo pasamos a GameManager?
    private GameManager gestorJuego;
   // private bool impactoNaveGrande = false;

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
        Debug.Log("Se ha producido un trigger con " + other.gameObject);

        //Impacto con NaveGrande
     /*   if (other.gameObject.name == "NaveGrande" && !impactoNaveGrande)
        {
            Debug.Log("IMPACTO CON NAVE_GRANDE: " + other.gameObject.name +
                this.gameObject.name+" "+impactoNaveGrande);
            
            impactoNaveGrande = true; //Para que no cuenten impactos dobles por la animación hasta que se destruye del todo
            
            //Animacion explosión nave enemiga
             Animator animator = other.GetComponent<Animator>();
             animator.SetTrigger("Impacto");

            //Esta nave da puntosBase x 5
            gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase*5);
            gestorJuego.ActualizarContadorEnemigosEliminados();
        }*/

        // Impacto con resto de enemigos
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("IMPACTO ENTRE: " + other.gameObject.name +
               this.gameObject.name);
            Destroy(other.gameObject);
            //Dan puntosBase x 2 el resto de enemigos
            gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase * 2);
            gestorJuego.UpdatePlayerType();
        }

        //Impacto entre proyectiles
        else if (other.gameObject.CompareTag("Bullet"))
        {
            //Dan 1/5 de los puntosBase los proyectiles destruidos
            gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase / 5);
            Destroy(other.gameObject);
        }

        //Proyectil fallido
        else if (other.name != "Player")
        {
            //Quitan 10% de los puntos base los disparos fallados
            gestorJuego.ActualizarContadorPuntuacion(-gestorJuego.puntosBase/10);
            
        }
        //Siempre destruimos el proyectil
        Destroy(this.gameObject);
    }
}
