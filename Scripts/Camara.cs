using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [Tooltip("GameObject al que debe seguir la c�mara")]
    public Transform objetivo = null;

    //A�adimos para cambiar el zoom de la c�mara durante el juego
    [Tooltip("Velocidad del zoom")]
    public float zoomSpeed;
    [Tooltip("Tama�o minimo de Size")]
    public float orthographicSizeMin;
    [Tooltip("Tama�o m�ximo de Size")]
    public float orthographicSizeMax;

    private Camera myCamera;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        PosicionarCamara();
        Zoom();
    }

    private void PosicionarCamara()
    {
        if (objetivo != null)
        {
            Vector3 nuevaPosicion = objetivo.position;
            nuevaPosicion.z = this.transform.position.z;

            transform.position = nuevaPosicion;
        }
    }

    //A�adimos poder hacer zoom con la rueda del rat�n
    //http://theflyingkeyboard.net/unity/unity-c-zoom-in-and-out-mouse-wheel-input/
    private void Zoom() 
    {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                myCamera.orthographicSize += zoomSpeed;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                myCamera.orthographicSize -= zoomSpeed;
            }
            myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, orthographicSizeMin, orthographicSizeMax);
    }
}
