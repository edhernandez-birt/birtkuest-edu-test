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
            Debug.Log("Toque entre: " + other.gameObject.name + " item: "+
               this.gameObject.name);
            gestorJuego.ActualizarContadorPuntuacion(gestorJuego.puntosBase * multiplicadorPuntosItem);
            //gestorJuego.UpdatePlayerType();            
        }
        Destroy(this.gameObject);
    }
}
