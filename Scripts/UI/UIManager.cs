using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Tooltip("P�ginas en la interfaz")]
    [SerializeField] private List<GameObject> pages;
    [SerializeField] private TextMeshProUGUI textoStats;

    private string player1;
    private string player2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) && player1 == null)
        {
            //Pedir nombre jugador 1
            GoToPage(1);
        }
        else if (Input.GetKey(KeyCode.Return) && player1 != null)
        {
            GoToPage(2);
        }
    }

    public void LoadLevelByName(string nombre)
    {
        if (nombre == "SceneEdu")
        {
            PlayerPrefs.SetString("gameMode", "1P");
        }else if (nombre == "SceneSplit")
        {
            PlayerPrefs.SetString("gameMode", "2P Split");
        }

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
        // Desactiva todas las p�ginas
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

    public void ReadSecondPlayerNameInput(string input)
    {
        player2 = input;
        PlayerPrefs.SetString("player2name", player2);
        Debug.Log(player2);
        LoadLevelByName("SceneSplit");
    }

    public void CargarListaPartidas()
    {
        textoStats.text = LeerPartidas.TopPlayersLoad();    
    }
}
