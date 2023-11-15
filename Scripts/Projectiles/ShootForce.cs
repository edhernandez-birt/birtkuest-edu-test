using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootForce : MonoBehaviour
{
    public float force = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rigidbody2d = GetComponent<Rigidbody2D>();

        rigidbody2d.AddForce(transform.up * force);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
