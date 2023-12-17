class Partida
{
    public string NombreJugador { get; }
    public int Puntos { get; }
    public int EnemigosEliminados { get; }

    public Partida(string nombre, int puntos, int enemigosEliminados)
    {
        NombreJugador = nombre;
        Puntos = puntos;
        EnemigosEliminados = enemigosEliminados;
    }
}