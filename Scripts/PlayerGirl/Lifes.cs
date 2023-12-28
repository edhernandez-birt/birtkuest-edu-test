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

    public void changeVidas(int cantidad, int id)
    {
        if ((vidasActuales + cantidad) < vidasMax)
        {
            vidasActuales += cantidad;
            gestorJuego.ActualizarContadorVidas(vidasActuales, id);
            Debug.Log("Vidas player " + id + " :" + vidasActuales);
        }

        if (vidasActuales == 0)
        {
            Animator animator = this.GetComponent<Animator>();
            animator.SetTrigger("Dead");
            //Modo 1P fin de juego directamente
            if (PlayerPrefs.GetString("gameMode") == "1P")
            {
                gestorJuego.FinJuego("dead");
            } //Modo Split solo fin de juego si ya ha muerto antes el otro jugador
            else if (PlayerPrefs.GetString("gameMode") == "2P Split")
            { 
                if (PlayerPrefs.GetInt("playersDead")==1)
                {
                    gestorJuego.FinJuego("dead");
                }
                //Guardamos en prefs que ya ha muerto un jugador
                int deadCounter = PlayerPrefs.GetInt("playersDead");
                deadCounter++;
                PlayerPrefs.SetInt("playersDead", deadCounter);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gestorJuego = FindObjectOfType<GameManager>();
        vidasActuales = gestorJuego.NumVidas;
        vidasMax = vidasActuales;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
