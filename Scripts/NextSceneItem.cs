using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Se ha producido un trigger con " + other.gameObject + " de " + this.gameObject);

        PlayerController playerController = other.GetComponent<PlayerController>();

        //Se pueden añadir más condiciones para que pase a la siguiente pantalla
        if (playerController != null)
        {
            SceneManager.LoadScene(sceneName);
            Destroy(this.gameObject);
        }

    }
}
