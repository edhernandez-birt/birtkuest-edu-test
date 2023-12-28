
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

class GuardarPartida
{
    private static string rutaBBDD = @".\BBDD.txt";

    public static void GuardarDatosPartida() {

            try
            {
                File.AppendAllText(rutaBBDD,
                PlayerPrefs.GetString("player1name") + ";" +
                PlayerPrefs.GetInt("totalPoints").ToString() + ";" +
                PlayerPrefs.GetInt("totalEnemies").ToString() +
                Environment.NewLine);
            if (PlayerPrefs.GetString("gameMode")=="2P Split")
            {
                File.AppendAllText(rutaBBDD,
                PlayerPrefs.GetString("player2name") + ";" +
                PlayerPrefs.GetInt("totalPoints2").ToString() + ";" +
                PlayerPrefs.GetInt("totalEnemies").ToString() +
                Environment.NewLine);
            }
            }
            catch (Exception e)
            {
                Debug.Log("Error al guardar datos partida: " + e.Message);
            }
        
    }
   
}
