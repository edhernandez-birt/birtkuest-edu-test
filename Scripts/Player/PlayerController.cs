using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private GameObject proyectilPrefab;

    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;

    //Variables para controlar que solo tire proyectiles cada X segundos
    private bool shootAllowed = true;
    private float nextShootTime = 0f;

    private float aimX = 0f;
    private float aimY = 0f;


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

        //Sumamos tiempo en el contador
        nextShootTime += Time.deltaTime;

        //Si podemos disparar y pulsamos el botón de disparo
        if (shootAllowed && Input.GetButtonDown("Fire1"))
        {
            // Damos valores para la animación de apuntado
            playerAnimator.SetFloat("AimHorizontal", moveX);
            aimX = moveX;
            playerAnimator.SetFloat("AimVertical", moveY);
            aimY = moveY;
            // playerAnimator.SetFloat("AimMagnitude", moveInput.magnitude);

            LanzarDespuesDeAnimacion();
        }
        //Si ha pasado más de un segundo desde el último disparo volvemos a permitir disparo
        if (nextShootTime > 1.0f)
        {
            shootAllowed = true;
        }


    }

    private void FixedUpdate()
    {
        //Físicas
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);

    }

    //https://www.youtube.com/watch?v=aXtd5KFf_iE
    //private void AimAndShoot()
    private void AimAndShoot(float aimX, float aimY)
    {
        Vector2 shootingDirection = new Vector2(aimX, aimY);
        if (shootingDirection.magnitude > 0.0f)
        {
            shootingDirection.Normalize();
        }

        //Corrutina para esperar a la animación?
        GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);
        proyectil.GetComponent<Rigidbody2D>().velocity = shootingDirection * 5.0f;
        proyectil.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
        Destroy(proyectil, 3.0f);
        // }

    }

    public void LanzarDespuesDeAnimacion()
    {
        StartCoroutine(LanzarDespuesDeAnimacionCoroutine());
    }

    IEnumerator LanzarDespuesDeAnimacionCoroutine()
    {
        // Reproducir la animación
        playerAnimator.SetBool("Aim", true);
        //No dejamos tirar más proyectiles hasta que pase un tiempo
        shootAllowed = false;
        nextShootTime = 0.0f;

        // Damos tiempo a que termine la animación de disparo
        yield return new WaitForSeconds(0.75f);

        // Lanzamos el objeto
        AimAndShoot(aimX, aimY);

        //Cancelamos la animación de apuntado
        playerAnimator.SetBool("Aim", false);

    }
}
