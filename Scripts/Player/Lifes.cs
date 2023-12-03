using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifes : MonoBehaviour
{
    int vidasActuales;
    int vidasMax;
    GameManager gestorJuego;

    public int getVidas()
    {
        return this.vidasActuales;
    }

    public void setVidas(int vidas)
    {
        this.vidasActuales = vidas;
    }

    public int getVidasMax()
    {
        return this.vidasMax;
    }

    public void setVidasMax(int vidasMax)
    {
        this.vidasMax = vidasMax;
    }


    public void changeVidas(int cantidad)
    {
        if ((vidasActuales + cantidad) < vidasMax)
        {
            vidasActuales += cantidad;
            gestorJuego.ActualizarContadorVidas(vidasActuales);
        }
       
        if (vidasActuales == 0)
        {
            Animator animator = this.GetComponent<Animator>();
            animator.SetTrigger("Dead");
            gestorJuego.FinJuego("dead");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        gestorJuego = FindObjectOfType<GameManager>();
        vidasActuales = gestorJuego.numVidas;
        vidasMax = vidasActuales;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
