using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ShowMessages : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Image messageImage;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Se ha producido un trigger con " + other.gameObject + " de " + this.gameObject);

        PlayerControllerGirl controller = other.GetComponent<PlayerControllerGirl>();

        if (controller != null)
        {
            messageText.enabled = true;
            messageImage.enabled = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Se ha producido un trigger Exit en " + this.gameObject);
        messageText.enabled = false;
        messageImage.enabled = false;
    }

}
