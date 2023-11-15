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

        // Vector3 aim = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0.0f);
        playerAnimator.SetFloat("AimHorizontal", moveX);
        playerAnimator.SetFloat("AimVertical", moveY);
        playerAnimator.SetFloat("AimMagnitude", moveInput.magnitude);
        playerAnimator.SetBool("Aim", Input.GetButtonDown("Fire1"));

        AimAndShoot();
    }

    private void FixedUpdate()
    {
        //Físicas
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);

    }

    //https://www.youtube.com/watch?v=aXtd5KFf_iE
    private void AimAndShoot()
    {
        Vector2 shootingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (shootingDirection.magnitude > 0.0f)
        {
            shootingDirection.Normalize();
        }
            if (Input.GetButtonDown("Fire1"))
            {
            //Corrutina para esperar a la animación?
            GameObject proyectil = Instantiate(proyectilPrefab,transform.position,Quaternion.identity);
                proyectil.GetComponent<Rigidbody2D>().velocity = shootingDirection *5.0f;
                proyectil.transform.Rotate(0,0,Mathf.Atan2(shootingDirection.y,shootingDirection.x)*Mathf.Rad2Deg);
                Destroy(proyectil, 3.0f);
            }
    }

    public void LanzarDespuesDeAnimacion()
    {
        StartCoroutine(LanzarDespuesDeAnimacionCoroutine());
    }

    IEnumerator LanzarDespuesDeAnimacionCoroutine()
    {
        // Reproducir la animación
        playerAnimator.SetTrigger("Aim");

        // Esperar a que termine la duración de la animación actual
        yield return new WaitForSeconds(0.9f);

        // Lanzar el objeto
        AimAndShoot();
    }
}
