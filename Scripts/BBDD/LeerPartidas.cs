
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class LeerPartidas
{
    private static string rutaBBDD = @".\BBDD.txt";

    public static string TopPlayersLoad()
    {
        string formatedText = "";

        List<Partida> listaPartidas = new List<Partida>();
        if (File.Exists(rutaBBDD))
        {
            try
            {
                string[] datosLeidos = File.ReadAllLines(rutaBBDD);
                foreach (string linea in datosLeidos)
                {
                    string[] datos = linea.Split(';');

                    if (datos.Length == 3)
                    {
                        string nombre = datos[0];
                        int puntos;
                        int enemigosEliminados;

                        if (int.TryParse(datos[1], out puntos) && int.TryParse(datos[2], out enemigosEliminados))
                        {
                            Partida partida = new Partida(nombre, puntos, enemigosEliminados);
                            listaPartidas.Add(partida);
                        }
                        else
                        {
                            Debug.Log($"Error en el formato de línea: {linea}");
                        }
                    }
                    else
                    {
                        Debug.Log($"Formato de línea incorrecto: {linea}");
                    }
                }

                // Ordenar las partidas por puntos de forma descendente
                List<Partida> listaPartidasOrdenadas = listaPartidas.OrderByDescending(j => j.Puntos).ToList();
                listaPartidas = listaPartidasOrdenadas;
                // Mostrar las partidas ordenadas por puntos
                Debug.Log("Partidas ordenadas por puntos:");
                //formatedText = ("#### TOP GAMES ####")+Environment.NewLine;
                //formatedText += (" NAME -- POINTS -- ENEMIES")+Environment.NewLine;

                //foreach (Partida partida in listaPartidasOrdenadas)
                //{
                //    Debug.Log($"{partida.NombreJugador}: Puntos - {partida.Puntos}, Enemigos eliminados - {partida.EnemigosEliminados}");
                //    formatedText += ($"{partida.NombreJugador}: Puntos - {partida.Puntos}, Enemigos eliminados - {partida.EnemigosEliminados}")+Environment.NewLine;
                //}
                int limitePartidas = 5; //Hacerlo configurable desde Unity?

                if (listaPartidasOrdenadas.Count < limitePartidas)
                {
                    limitePartidas = listaPartidasOrdenadas.Count;
                }

                for (int i = 0; i < limitePartidas; i++)
                {
                    Partida partida = listaPartidasOrdenadas.ElementAt(i);

                    //Inicio

                    //Debug.Log($" {i}. {partida.NombreJugador}: Puntos - {partida.Puntos}, Enemigos eliminados - {partida.EnemigosEliminados}");
                    // formatedText += ($"{i+1}. {partida.NombreJugador}: -- {partida.Puntos}, -- {partida.EnemigosEliminados}") + Environment.NewLine;  

                    //Mejora
                    //formatedText +=($"{partida.NombreJugador,-15} {partida.Puntos,-12} {partida.EnemigosEliminados,-10}")+Environment.NewLine;

                    //formatedText += string.Format(("{0,-12}{1,-15}{2,4}"),partida.NombreJugador, partida.Puntos, partida.EnemigosEliminados)+Environment.NewLine;

                    //Hay que usar letra tipo "consola" Si no descuadra

                    formatedText += $"{partida.NombreJugador.PadRight(10)} ";
                    formatedText += $"{partida.Puntos,5}";
                    formatedText += $"{partida.EnemigosEliminados,10}" + Environment.NewLine;

                    Debug.Log($"{partida.NombreJugador.PadRight(15).Substring(0, Math.Min(partida.NombreJugador.Length, 15)),-15} " +
                        $"{partida.Puntos.ToString().PadLeft(12),-12} " +
                        $"{partida.EnemigosEliminados.ToString().PadLeft(10),-10}" + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Excepcion: " + e.Message);
            }
            return formatedText;

        }
        else
        {
            Debug.Log("No hay datos de partidas");
            return "----------".PadRight(30);
        }

    }
}
