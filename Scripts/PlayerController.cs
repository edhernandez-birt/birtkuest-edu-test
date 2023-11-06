using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;

    [SerializeField] private GameObject proyectilPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;//Para igualar velocidad diagonal
        playerAnimator.SetFloat("MoveHorizontal", moveX);
        playerAnimator.SetFloat("MoveVertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);


        AimAndShoot();

        // Vector3 aim = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0.0f);
        playerAnimator.SetFloat("AimHorizontal", moveX);
        playerAnimator.SetFloat("AimVertical", moveY);
        playerAnimator.SetFloat("AimMagnitude", moveInput.magnitude);
        playerAnimator.SetBool("Aim", Input.GetButton("Fire1"));


    }

    private void FixedUpdate()
    {
        //Físicas
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);

    }

    //https://www.youtube.com/watch?v=aXtd5KFf_iE
    private void AimAndShoot()
    {
     //   Vector3 aim = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector2 shootingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
     //   if (aim.magnitude > 0.0f)
     //   {
           // aim.Normalize();
         shootingDirection.Normalize();

            if (Input.GetButtonDown("Fire1"))
            {
                GameObject proyectil = Instantiate(proyectilPrefab,transform.position,Quaternion.identity);
                proyectil.GetComponent<Rigidbody2D>().velocity = shootingDirection *5.0f;
                proyectil.transform.Rotate(0,0,Mathf.Atan2(shootingDirection.y,shootingDirection.x)*Mathf.Rad2Deg);
                Destroy(proyectil, 3.0f);
            }
      //  }
    }
}
