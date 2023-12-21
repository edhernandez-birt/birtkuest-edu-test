using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class PlayerControllerGirl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    //public InputSystemUIInputModule inputModule;
    //[SerializeField] private Transform bodyTransform;
    //[SerializeField] private Rigidbody2D myRigidbody;
    //[SerializeField] private Animator myAnimator;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private int playerId;
   // [SerializeField] private GameObject proyectilPrefab;
    public Vector2 LastDirection;
    private Vector2 previousMovementInput;
    //[SerializeField] private float speed = 3f;

    private Rigidbody2D myRigidbody;
    //private Vector2 moveInput;
    private Animator myAnimator;

    public int PlayerId { get => playerId; set => playerId = value; }

    //Variables para controlar que solo tire proyectiles cada X segundos
    // private bool shootAllowed = true;
    // private float nextShootTime = 0f;

    // private float aimX = 0f;
    // private float aimY = 0f;

    //public void Spawn()
    //{

    //    inputReader.MoveEvent += HandleMove;

    //    LastDirection = -1 * transform.up;
    //}
    //public void Despawn()
    //{
    //    inputReader.MoveEvent -= HandleMove;
    //}

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        
    }


    private void Update()
    {
        if (playerId == 1)
        {   //New input
            inputReader.MoveEvent += HandleMove;
        }else if (playerId == 2)
        {   //Old input
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            Vector2 moveInput = new Vector2(moveX, moveY).normalized;
            HandleMove(moveInput);
        }
    }

    private void FixedUpdate()
    {

        if (!myAnimator.GetBool("isLaunching"))
        {
            myRigidbody.velocity = previousMovementInput * movementSpeed;
        }
        else
        {
            myRigidbody.velocity = Vector3.zero;
        }

        if (myRigidbody.velocity.magnitude > 0.0001)
        {
            myAnimator.SetBool("isMoving", true);
            Vector2 moveInput = myRigidbody.velocity.normalized;
            myAnimator.SetFloat("moveX", moveInput.x);
            myAnimator.SetFloat("moveY", moveInput.y);
        }
        else
        {
            if (!myAnimator.GetBool("isLaunching"))
            {
                myAnimator.SetBool("isMoving", false);
            }
        }

    }

    private void HandleMove(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
        if (movementInput.magnitude > 0.1 && myAnimator!=null)
        {
            LastDirection = movementInput;
            Vector2 moveInput = movementInput.normalized;
            myAnimator.SetFloat("aimX", moveInput.x);
            myAnimator.SetFloat("aimY", moveInput.y);
        }
    }

    private void HandleMoveOld(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
        if (movementInput.magnitude > 0.1 && myAnimator != null)
        {
            LastDirection = movementInput;
            Vector2 moveInput = movementInput.normalized;
            myAnimator.SetFloat("aimX", moveInput.x);
            myAnimator.SetFloat("aimY", moveInput.y);
        }
    }
}

#region OldCode

// Update is called once per frame
//void Update()
//{
//    float moveX = Input.GetAxisRaw("Horizontal");
//    float moveY = Input.GetAxisRaw("Vertical");
//    moveInput = new Vector2(moveX, moveY).normalized;//Para igualar velocidad diagonal
//    playerAnimator.SetFloat("MoveHorizontal", moveX);
//    playerAnimator.SetFloat("MoveVertical", moveY);
//    playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);

//    //Sumamos tiempo en el contador
//    nextShootTime += Time.deltaTime;

//    //Si podemos disparar y pulsamos el botón de disparo
//    if (shootAllowed && Input.GetButtonDown("Fire1"))
//    {
//        // Damos valores para la animación de apuntado
//        playerAnimator.SetFloat("AimHorizontal", moveX);
//        aimX = moveX;
//        playerAnimator.SetFloat("AimVertical", moveY);
//        aimY = moveY;
//        // playerAnimator.SetFloat("AimMagnitude", moveInput.magnitude);

//        LanzarDespuesDeAnimacion();
//    }
//    //Si ha pasado más de un segundo desde el último disparo volvemos a permitir disparo
//    if (nextShootTime > 1.0f)
//    {
//        shootAllowed = true;
//    }


//}

//private void FixedUpdate()
//{
//    //Físicas
//    playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);

//}

////https://www.youtube.com/watch?v=aXtd5KFf_iE
////private void AimAndShoot()
//private void AimAndShoot(float aimX, float aimY)
//{
//    Vector2 shootingDirection = new Vector2(aimX, aimY);
//    if (shootingDirection.magnitude > 0.0f)
//    {
//        shootingDirection.Normalize();
//    }

//    //Corrutina para esperar a la animación?
//    GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);
//    proyectil.GetComponent<Rigidbody2D>().velocity = shootingDirection * 5.0f;
//    proyectil.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
//    Destroy(proyectil, 3.0f);
//    // }

//}

//public void LanzarDespuesDeAnimacion()
//{
//    StartCoroutine(LanzarDespuesDeAnimacionCoroutine());
//}

//IEnumerator LanzarDespuesDeAnimacionCoroutine()
//{
//    // Reproducir la animación
//    playerAnimator.SetBool("Aim", true);
//    //No dejamos tirar más proyectiles hasta que pase un tiempo
//    shootAllowed = false;
//    nextShootTime = 0.0f;

//    // Damos tiempo a que termine la animación de disparo
//    yield return new WaitForSeconds(0.75f);

//    // Lanzamos el objeto
//    AimAndShoot(aimX, aimY);

//    //Cancelamos la animación de apuntado
//    playerAnimator.SetBool("Aim", false);

//}
#endregion
