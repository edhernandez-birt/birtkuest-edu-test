using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DestroySelfOnContact : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator objectAnimator;
    [SerializeField] private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(objectAnimator != null)
        {
            Destroy(gameObject, 1);
            objectAnimator?.SetBool("collision", true);
        }
        if(rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        if(objectAnimator == null)
        {
            Destroy(gameObject);
        }
        //Destroy(gameObject, 1);
        //objectAnimator?.SetBool("collision", true);
        //if (rb != null)
        //{
        //    rb.velocity = Vector2.zero;
        //}
    }

}
