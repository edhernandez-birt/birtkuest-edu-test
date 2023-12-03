using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; //Para TextMeshPro
using System;
using System.IO;
using UnityEditor.Localization.Editor;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;


public class GameManager : MonoBehaviour
{
    #region Atributos
    // Interfaz
    public TextMeshProUGUI textoFin;
    public Image fondoFin;
    //public TextMeshProUGUI vidasText;
    public TextMeshProUGUI vidasNum;
    public TextMeshProUGUI puntosText;
    public TextMeshProUGUI tiempoText;
  //  public TextMeshProUGUI textoVelocidad;
    public TextMeshProUGUI textoEnemigos;
    public TextMeshProUGUI textoRecord;
  //  public Button botonReiniciar;
  //  public Button botonOtraEscena;



    // Configuración
    public int numVidas = 3;
    public int puntosBase = 100;
    private int puntosTotales = 0;
    private float tiempo = 0.0f;
    private int enemigosEliminados = 0; //Enemigos eliminados 0 al inicio
    private int hiscore = 500; //Record inicial por si no hay fichero
    private static string rutaFichero = @".\Record.txt";

    //Audio 
  //  public AudioClip audioVictoria;
    public AudioClip audioDead;
    private AudioSource source;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Contamos enemigos
       // GameObject[] listaEnemigos = GameObject.FindGameObjectsWithTag("Enemy");
        //Sumamos uno porque el método de actualizar resta uno siempre
        enemigosEliminados = 0;
       // ActualizarContadorEnemigos();

        //En el arranque quitamos mensajes de final de UI
        textoFin.enabled = false;
        fondoFin.enabled = false;
     //   botonReiniciar.gameObject.SetActive(false);
     //   botonOtraEscena.gameObject.SetActive(false);
        //Actualizamos marcador de vidas y puntos
        vidasNum.text = "" + numVidas;
          //Vidas.Text.text = LocalizationSettings.
        puntosText.text = "Puntos: " + puntosTotales;

        //Leemos record de fichero
        hiscore = int.Parse(LeerRecordFichero());
        textoRecord.text = "HiScore: "+hiscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando se acabe el juego que se pare el temporizador
        if (!textoFin.enabled){
            ActualizarContadorTiempo();
        }
    }
    #region Actualizar marcadores
    //Métodos para actulizar marcadores
    public void ActualizarContadorVidas(int vidas)
    {
        vidasNum.text = ""+vidas;
    }

    public void ActualizarContadorPuntuacion(int puntos)
    {
        puntosTotales += puntos;
        puntosText.text = "Puntos: " + puntosTotales;
    }

    private void ActualizarContadorTiempo()
    {
        //https://forum.unity.com/threads/creating-a-simple-timer.328409/
        tiempo += Time.deltaTime;
     //   tiempoText.text = "Tiempo: "+tiempo.ToString("#0.0");
     tiempoText.text = "Tiempo: " + tiempo.ToString("#0");
    }


    /// <summary>
    /// Método para actualizar los enemigos pendientes.
    /// Se llamará cada vez que se destruya uno de ellos
    /// </summary>
    public void UpdatePlayerType()
    {
        //Contamos un enemigo eliminado
        enemigosEliminados += 1;

        if (enemigosEliminados <= 3)
        {
            textoEnemigos.text = "Pacifista";
        }
        else if(enemigosEliminados >3 && enemigosEliminados < 10){
            textoEnemigos.text = "Agresivo";
        }else
        {
            textoEnemigos.text = "Sanguinario";
        }
    }  

    private void ActualizarContadorRecord()
    {
        if (puntosTotales > hiscore)
        {
            hiscore = puntosTotales;
            textoRecord.text = "HiScore: "+hiscore.ToString();
            //textoFin.text = textoFin.text + "\nNUEVO RECORD " + hiscore +" PUNTOS";
            EscribirRecordFichero();
        }
    }
    #endregion

    #region Gestion fichero record
    /// <summary>
    /// Método para escribir la puntuación máxima en un fichero de texto
    /// </summary>
    private void EscribirRecordFichero()
    {
        try
        {
            File.WriteAllText(rutaFichero, hiscore.ToString());
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
        }
    }

    /// <summary>
    /// Metodo para leer el fichero de record
    /// </summary>
    /// <returns></returns>
    private string LeerRecordFichero()
    {
        string recordFichero = hiscore.ToString();
        if (File.Exists(rutaFichero))
        {
        string[] datosLeidos = File.ReadAllLines(rutaFichero);
       
            try
            {
                File.ReadAllLines(rutaFichero);
                recordFichero = datosLeidos[0];
                Debug.Log(recordFichero);
            }
            catch (Exception e)
            {
                Debug.Log("Excepcion: " + e.Message);
            }
        }
        return recordFichero;
    }
    #endregion

    #region Fin de juego
    /// <summary>
    /// Añado parametro a FinJuego para en caso de victoria mostrar otro mensaje
    /// </summary>
    /// <param name="motivo"></param>
    public void FinJuego(string motivo)
    {
 
        //textoFin.text = "GAME OVER"; Darle una vuelta por idiomas

          source = GetComponent<AudioSource>();
          if (motivo == "dead")
          {
            //  textoFin.text = "GAME OVER";
            //Cambio de audio
            source.volume = 0.5f;
              source.clip = audioDead;
              source.PlayDelayed(0.5f);
             // source.PlayOneShot(audioDead, 0.5f);
             //Eliminamos enemigos
              GameObject[] listaEnemigos = GameObject.FindGameObjectsWithTag("Enemy");
              foreach (GameObject enemigo in listaEnemigos)
              {
                  Destroy(enemigo);
              }
          } /* else if (motivo == "victoria")
          {
              textoFin.text = "FIN";

              source.PlayOneShot(audioVictoria, 0.5f);

              //Eliminamos nuestra nave
              GameObject[] nuestraNave = GameObject.FindGameObjectsWithTag("Player");
              Destroy(nuestraNave[0]);

              //Eliminamos enemigos si quedaran
              GameObject[] listaEnemigos = GameObject.FindGameObjectsWithTag("Enemy");
              foreach (GameObject enemigo in listaEnemigos )
              {
                  if (enemigo.gameObject.name != "NaveGrande")
                  {
                      Destroy(enemigo);
                  }
              }

              // Si ganamos esta escena, activo el botón para jugar a la otra escena
              botonOtraEscena.gameObject.SetActive(true);
          }*/

          //Siempre eliminamos proyectiles que hubiera
          GameObject[] listaProyectiles = GameObject.FindGameObjectsWithTag("Bullet");
          foreach (GameObject proyectil in listaProyectiles)
          {
              Destroy(proyectil);
          }

        textoFin.enabled = true;
        fondoFin.enabled = true;
        
          //Comprobamos si ha habido nuevo record solo si hemos ganado
          ActualizarContadorRecord();

          //Siempre habilitamos el botón de reiniciar
          //botonReiniciar.gameObject.SetActive(true);
    }
    #endregion
}