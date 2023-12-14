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

 //   [SerializeField] float timeBtwnChars;
 //   [SerializeField] float timeBtwnWords;

    void Start()
    {
    }

    //Mostrar letra a letra. No va bien después del primer intento
    /*
    public void EndCheck()
    {
            StartCoroutine(TextVisible());
    }

    private IEnumerator TextVisible()
    {
        messageText.ForceMeshUpdate();
        int totalVisibleCharacters = messageText.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            messageText.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                Invoke("EndCheck", timeBtwnWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);


        }
    }*/

    /*
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
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Se ha producido una colision con " + this.gameObject);

        {
            messageText.enabled = true;
            messageImage.enabled = true;
           // EndCheck();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Se ha dejado de producir una colision con " + this.gameObject);

        {
            messageText.enabled = false;
            messageImage.enabled = false;
        }
    }

}
