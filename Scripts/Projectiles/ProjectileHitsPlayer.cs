using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilAlcanzaPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Colisión con Player
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Se ha producido un trigger con " + other.gameObject +" de "+this.gameObject);

        Lifes controller = other.GetComponent<Lifes>();

        if (controller != null)
        {
            controller.changeVidas(-1,other.GetComponent<PlayerControllerGirl>().PlayerId);
            int vidas = controller.getVidas();
            Debug.Log("Vidas: "+vidas);

        }

        Destroy(this.gameObject);
    }
}
