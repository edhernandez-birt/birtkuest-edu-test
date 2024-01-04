using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLifes : MonoBehaviour
{
    [Tooltip("The prefab image to use when displaying lives remaining")]
    public GameObject imagen;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLifesImages(int num)
    {
        // Se destruyen las vidas actuales

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }



        // Se crean las nuevas
        for (int i = 0; i < num; i++)
        {
            Instantiate(imagen, transform);
        }
    }
}
