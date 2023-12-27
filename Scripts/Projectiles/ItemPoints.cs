using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoints : MonoBehaviour
{
    private GameManager gestorJuego;
    public int multiplicadorPuntosItem;

    // Start is called before the first frame update
    void Start()
    {
        gestorJuego = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Colisión con items para conseguir puntaciones diversas

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Se ha producido un trigger con " + other.gameObject);
       
        // Impacto con player para que sume puntos
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerControllerGirl playerControllerGirl = other.GetComponent<PlayerControllerGirl>();

            Debug.Log("Toque entre: " + other.gameObject.name + " item: "+ this.gameObject.name);

            gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase * multiplicadorPuntosItem, playerControllerGirl.PlayerId);

            //if (playerControllerGirl.PlayerId == 1)
            //{
            //    gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase * multiplicadorPuntosItem, 1);
            //}
            //else if (playerControllerGirl.PlayerId == 2)
            //{
            //    gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase * multiplicadorPuntosItem, 2);
            //}
                //gestorJuego.UpdatePlayerType();            
        }
        Destroy(this.gameObject);
    }
}
