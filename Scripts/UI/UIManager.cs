using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Tooltip("Páginas en la interfaz")]
    public List<GameObject> pages;
    public AudioSource sonidoFondo;

 //   [SerializeField] Image userInputBackground;

    private int pause = 0;

    private string player1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pausar();
            Debug.Log("Escena en pausa");
        }

        if (Input.GetKey(KeyCode.Return) && player1==null)
        {
            GoToPage(1);
     //       userInputBackground.enabled = true;
            //Pedir nombre jugador 1
        }else if (Input.GetKey(KeyCode.Return) && player1!=null)
        {
            GoToPage(2);
        }
    }

    // Congela la partida modificando el valor de Time.timeScale a 0
    public void pausar()
    {
        Time.timeScale = 0;
        pages[pause].SetActive(true);
    }

    // Descongela la partida modificando el valor de Time.timeScale a 1
    public void Reanudar()
    {
        Time.timeScale = 1;
        pages[pause].SetActive(false);
    }


    // Cambia la configuración del sonido y la guarda en local
    //public void CambiarSonido()
    //{
    //    sonido = -sonido;
    //    sonidoFondo.enabled = !sonidoFondo.enabled;
    //    PlayerPrefs.SetInt("sonido", sonido);
    //}

    public void LoadLevelByName(string nombre)
    {
        Time.timeScale = 1;         // Para que no se quede en pause
        SceneManager.LoadScene(nombre);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GoToPage(int pageIndex)
    {
        // Desactiva todas las páginas
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        // Carga la seleccionada
        pages[pageIndex].SetActive(true);
    }

    public void ReadOnePlayerNameInput(string input)
    {
        player1 = input;
        PlayerPrefs.SetString("player1name", player1);
        Debug.Log(player1);
    }
}
