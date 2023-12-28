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
            Debug.Log("LIFES ACTUALES== 0");
            Animator animator = this.GetComponent<Animator>();
            animator.SetTrigger("Dead");
            
            if (PlayerPrefs.GetString("gameMode") == "1P")
            {
                gestorJuego.FinJuego("dead");
                Debug.Log("Sin vidas modo 1 player");
            } else if (PlayerPrefs.GetString("gameMode") == "2P Split")
            { //Fin de juego si los dos jugadores sin vida
                Debug.Log("LIFES ACTUALES 2P SPLIT == 0");
                if (PlayerPrefs.GetInt("playersDead")==1)
                {
                    gestorJuego.FinJuego("dead");
                    Debug.Log("Sin vidas modo 2 player SPLIT");
                }
                //Guardamos en prefs que ya ha muerto un jugador
                PlayerPrefs.SetInt("playersDead", 1);
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
