using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string sceneName;
    // Start is called before the first frame update

    //public SceneChange(string aName)
    //{
    //    sceneName = aName;
    //}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //Carga una escena con un nombre al pulsar enter
        if (Input.GetKey(KeyCode.Return))
        {
            SceneLoad(sceneName);
        }
    }

    private void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
