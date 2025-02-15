using Runner;
using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance;

    private const string ANIMATOR_IDLE = "Idle";
    private const string ANIMATOR_SPEED = "Speed";
    private const string ANIMATOR_FAINT = "Faint";
    private const string ANIMATOR_AGACHARSE = "Agacharse";

    [SerializeField]
    private CapsuleCollider collider;

    [SerializeField]
    private PlayerConfiguration playerConfiguration;

    [SerializeField]
    private Animator animator;

    private float alturaRay = 0.7f;

    private Rigidbody rb;
    private float horizontalAxis;
    private float verticalAxis;

    private bool isGrounded = true;
    private float playerVerticalVelocity = 0f;
    private bool canCheckGround = true;
    private float floorPoint;

    //Lanes
    private int laneIndex = 0;
    private int laneCenterPosition = 3;


    private float initialGravity;

    private float initForwardSpeed;
    public float currentForwardSpeed;
    private bool alive = true;
    private int caseControl;
    private bool isAgachado = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start() {
        
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        initialGravity = playerConfiguration.Gravity;

        caseControl = 1;//opcion de switch para alternar los controles

        currentForwardSpeed = initForwardSpeed = playerConfiguration.PlayerForwardSpeed;

        StopPlayerMovement();
        StartCoroutine(ResetPlayerMovementCoroutine());

    }

    private void Update() {
        //Inputs
        verticalAxis = PlayerInputManager.Instance.GetVerticalMovement();
        horizontalAxis = PlayerInputManager.Instance.GetHorizontalMovement();
        switch(caseControl)//podria haber sido un bool tambien
        {
            case 1:
                if (PlayerInputManager.Instance.IsLeftPressed())
                {
                    laneIndex--;
                }
                else if (PlayerInputManager.Instance.IsRightPressed())
                {
                    laneIndex++;
                }
                break;
            case 2://igual que el funcionamiento normal, pero al pulsar derecha se resta, y al pulsar izquierda se suma para alternarlo
                if (PlayerInputManager.Instance.IsLeftPressed())
                {
                    laneIndex++;
                }
                else if (PlayerInputManager.Instance.IsRightPressed())
                {
                    laneIndex--;
                }
                break;
            default://opciones default para controlar posibles errores
                print("case de cambio de control no reconocido");
                break;
        }
        
        laneIndex = Mathf.Clamp(laneIndex, -1, 1);

        JumpInput();
        Agacharse();

    }

    public void TurnControll()//alterna la opcion del switch de invertir controles
    {
        if(caseControl == 1)
        {
            caseControl = 2;

        }else
        {
            caseControl = 1;
        }
    }

    private void JumpInput() {
        if (isGrounded && !isAgachado) {//comprueba tambien si esta agachado ya que agachado no puede saltar

            if (PlayerInputManager.Instance.IsJumpPressed()) {
                isGrounded = false;
                canCheckGround = false;
                playerVerticalVelocity = playerConfiguration.JumpForce;
                StartCoroutine(EnableGroundedRay());
            }
        }
    }

    private void FixedUpdate() {

        //print(PlayerController.Instance.playerHealth.runtimeValue);
        if (PlayerController.Instance.playerHealth.runtimeValue > 0)
        {
            
            if (CheckObstacle() && alive)
            {
                alive = false;
                PlayerController.Instance.SubstractHealth();
                //Quitar control al jugador.
                StopPlayerMovement();
                animator.SetBool(ANIMATOR_FAINT, true);
                StartCoroutine(LanzarPanel());
            }

            IsGrounded();
            ApplyGravity();
            //Move();
            RunnerMove();
            PlayerController.Instance.AddDistance();//va aumentando la distancia segun va avanzando
            
        }else//si no tiene mas vidas muestra el panel del game over
        {
            GameOver.Instance.ShowGameOver();
        }
        
    }

    private IEnumerator LanzarPanel()//lanzo una cuenta atras cada vez que se choca
    {
        yield return new WaitForSeconds(1.3f);//la empiezo un poco mas tarde para cuadrar tiempos de que haga la animacion de faint, regrese atras, etc.
        Countdown.Instance.Start();
    }

    private void IsGrounded() {
        if (!canCheckGround) {
            isGrounded = false;
            return;
        }

        Vector3 rayOrigin = transform.position;
        rayOrigin.y += 0.1f;

        RaycastHit hit;
        Ray groundedRay = new Ray(rayOrigin, -transform.up);
        int groundMask = LayerMask.GetMask("Floor");
        if (Physics.Raycast(groundedRay, out hit, 0.15f, groundMask, QueryTriggerInteraction.Ignore)) {
            Debug.DrawRay(rayOrigin, -transform.up, Color.green);
            floorPoint = hit.point.y;
            isGrounded = true;
        } else {
            Debug.DrawRay(rayOrigin, -transform.up, Color.red);
            isGrounded = false;
        }
    }

    private void ApplyGravity() {
        if (isGrounded) {
            playerVerticalVelocity = 0;
        } else {

            float gravityToApply = playerConfiguration.Gravity;
            if (playerVerticalVelocity >= playerConfiguration.JumpForce - playerConfiguration.JumpHover) {
                gravityToApply = gravityToApply * playerConfiguration.JumpHoverPercent;
            }

            playerVerticalVelocity -= gravityToApply * Time.fixedDeltaTime;

        }
    }

    private IEnumerator EnableGroundedRay() {
        yield return new WaitForSeconds(0.03f);
        canCheckGround = true;
    }

    private bool CheckObstacle() {
        Vector3 rayOrigin = transform.position;
        rayOrigin.y += alturaRay;
        rayOrigin.z += (transform.forward * 0.2f).z;
        Ray obstacleRay = new Ray(rayOrigin, transform.forward);

        int obstacleMask = LayerMask.GetMask("Obstacles");
        if (Physics.Raycast(obstacleRay, 0.35f, obstacleMask)) {
            Debug.DrawLine(rayOrigin, rayOrigin + transform.forward * 0.35f, Color.red);
            return true;
        } else {
            Debug.DrawLine(rayOrigin, rayOrigin + transform.forward * 0.35f, Color.green);
            return false;
        }
    }

    private void Move() {
        //Inputs relative to player direction
        Vector3 verticalMovement = transform.forward * verticalAxis; //Esto hace que avance automaticamente en el eje Z
        Vector3 horizontalMovement = transform.right * horizontalAxis;

        //Combined 
        Vector3 combinedMovement = verticalMovement + horizontalMovement;
        combinedMovement = Mathf.Clamp01(combinedMovement.sqrMagnitude) * combinedMovement.normalized;

        Vector3 moveDirection = new Vector3(combinedMovement.x, 0f, combinedMovement.z);
        moveDirection = moveDirection * currentForwardSpeed * Time.fixedDeltaTime;

        rb.velocity = moveDirection + (playerVerticalVelocity * transform.up);
    }

    private void RunnerMove() {
        Vector3 lanePosition = rb.position;
        lanePosition.x = laneIndex * laneCenterPosition;
        lanePosition += transform.forward;

        Vector3 forwardMove = Vector3.Lerp(rb.position, new Vector3(0f, 0f, lanePosition.z), Time.fixedDeltaTime * currentForwardSpeed);
        Vector3 move = Vector3.Lerp(rb.position, new Vector3(lanePosition.x, 0f, 0f), Time.fixedDeltaTime * playerConfiguration.PlayerChangeLaneSpeed);
        Vector3 verticalMove = Vector3.Lerp(rb.position, rb.position + (playerVerticalVelocity * transform.up), Time.deltaTime * 3f);

        Vector3 combinedMove = new Vector3(move.x, verticalMove.y, forwardMove.z);

        if (combinedMove.y <= 0f) {
            combinedMove.y = 0f;
            Vector3 fixedPosition = rb.position;
            fixedPosition.y = floorPoint;
            rb.MovePosition(fixedPosition);
        }

        rb.MovePosition(combinedMove);
        
    }

    public void StopPlayerMovement() {
        currentForwardSpeed = 0f;
        PlayerInputManager.Instance.IsInputAllowed = false;
        animator.SetFloat(ANIMATOR_SPEED, currentForwardSpeed);
    }

    public void ResetPlayerMovement()
    {
        StartCoroutine(ResetPlayerMovementCoroutine(true));
    }

    private IEnumerator ResetPlayerMovementCoroutine(bool resetLevel = false) {
        if (resetLevel)
        {
            yield return new WaitForSeconds(1f);

            //Reset level
            LevelGenerator.instance.ResetLevelLayout();
        
        }
        
        animator.SetBool(ANIMATOR_FAINT, false);

        yield return new WaitForSeconds(3f);
        currentForwardSpeed = initForwardSpeed;
        PlayerInputManager.Instance.IsInputAllowed = true;
        alive = true;

        animator.SetBool(ANIMATOR_IDLE, false);
        animator.SetFloat(ANIMATOR_SPEED, currentForwardSpeed);
#if !UNITY_EDITOR
        Debug.Log("");
#endif

    }

    private void Agacharse()
    {
        if(isGrounded)//si esta en el suelo
        {
            if (Input.GetKey(KeyCode.LeftControl))//y se esta pulsando la tecla necesaria
            {
                animator.SetBool(ANIMATOR_AGACHARSE, true);//hace la "animacion" de agacharse (se hace mas pequeño)
                collider.height = 0.6f;//le bajo el tamaño al collider
                collider.center = new Vector3(0, 0.5f, 0);//bajo su centro
                transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);//para hacerlo pequeño le bajo la escala
                alturaRay = 0.3f;//bajo la altura del raycast para que no lo detecte como cuando esta normal
                isAgachado = true;//digo que esta agachado para que no pueda saltar
            }
            else//si no se pulsa el control, devuelvo los valores iniciales de tamaño y escalas
            {
                animator.SetBool(ANIMATOR_AGACHARSE, false);
                collider.height = 2;
                collider.center = new Vector3(0, 1, 0);
                transform.localScale = new Vector3(1, 1, 1);
                alturaRay = 0.7f;
                isAgachado = false;
            }
        }
    }

    public void ResumePlayerMovement()
    {
        currentForwardSpeed = 0;
        animator.SetBool(ANIMATOR_IDLE, true);
        StopPlayerMovement();
        StartCoroutine(ResetPlayerMovementCoroutine());
        
    }

    private IEnumerator ResumePlayerMovementCoroutine()
    {
        StopPlayerMovement();
        yield return new WaitForSeconds(3);
    }
}
