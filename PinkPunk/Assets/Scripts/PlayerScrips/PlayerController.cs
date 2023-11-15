using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public  PlayerInput actionMan; 

    [Header("Variables de animación")]
    [SerializeField] private Animator playerAnimatorControler;

    [Header("Variables de movimiento")]
    [SerializeField] private float horizontalMove;
    [SerializeField] private float verticalMove;
    [SerializeField] private Vector3 playerInput;
    [SerializeField] private CharacterController player;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxPlayerSpeed;
    [SerializeField] private float acelerationPlayerSpeed;
    [SerializeField] private float deacelerationPlayerSpeed;
    [SerializeField] private bool isOnSlope = false;
    private Vector3 hitNormal;
    [SerializeField] private float slideVelocity;
    [SerializeField] private float slopeForceDown;
    private Vector3 movePlayer;
    private Vector3 lastInputDirection = Vector3.zero;
    [Header("Variables de camara")]
    [SerializeField] private Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    [Header("Variables de salto")]
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float originalGravity;
    [SerializeField] private float fallVelocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool doubleJump;

    [Header("variables de Wall Run")]
    [SerializeField] private bool isWallRunning = false;
    [SerializeField] private bool wasWallRunning = false;
    [SerializeField] private float wallRuningGravity;
    [SerializeField] private float wallRuningForce;
    [SerializeField] private float wallRunSpeed;
    [SerializeField] private float normalSpeed;
    private Vector3 wallDirection;
    private Vector3 wallForce;

    [Header("variables de Wall Jump")]
    [SerializeField] private Vector3 wallNormal;
    [SerializeField] private float wallJumpingForce;

    [SerializeField] private float wallJumpJumpForce;
    

    [Header("variables de raycast")]
    [SerializeField] private float rayAngle;
    [SerializeField] private float rayCount;
    [SerializeField] private float rayDistance;

    [SerializeField] private LayerMask hitLayers;


    // Start is called before the first frame update
    void Start()
    {
        actionMan = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GetComponent<CharacterController>();
        playerAnimatorControler = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = actionMan.actions["Move"].ReadValue<Vector2>().x;
        verticalMove = actionMan.actions["Move"].ReadValue<Vector2>().y;

        playerInput = new Vector3(horizontalMove,0 ,verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        playerAnimatorControler.SetFloat("onMove",playerInput.magnitude * playerSpeed);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        if (playerInput.magnitude > 0)
        {
            lastInputDirection = movePlayer.normalized;
            playerSpeed = Mathf.Lerp(playerSpeed, maxPlayerSpeed, Time.deltaTime * acelerationPlayerSpeed);
        }
        else
        {
            playerSpeed = Mathf.Lerp(playerSpeed, 0, Time.deltaTime * deacelerationPlayerSpeed);
        }

        movePlayer = lastInputDirection * playerSpeed;


        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();
        PlayerSkills();

        player.Move( movePlayer * Time.deltaTime);

    }

    public void camDirection() 
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void PlayerSkills()
    {
        WallRun();
        JumpingPlayer();
    }

    
    public void WallRun()
    {
    wasWallRunning = isWallRunning;
    Vector3 rightDirection = transform.right;
    Vector3 leftDirection = -transform.right;
    Vector3 rayStartPosition = transform.position + (Vector3.up * (player.height * 0.5f));

    bool rightRayHitWall = false;
    bool leftRayHitWall = false;
    for (int i = 0; i < rayCount; i++)
        {
            float angle = Mathf.Lerp(-rayAngle / 2f, rayAngle / 2f, (float)i / (rayCount - 1));
            
            Vector3 rayDirectionRight = Quaternion.Euler(0, angle, 0) * rightDirection;
            Vector3 rayDirectionLeft = Quaternion.Euler(0, -angle, 0) * leftDirection;
            
            Ray rayRight = new Ray(rayStartPosition, rayDirectionRight);
            Ray rayLeft = new Ray(rayStartPosition, rayDirectionLeft);

            Debug.DrawRay(rayRight.origin, rayDirectionRight * rayDistance, Color.green);
            Debug.DrawRay(rayLeft.origin, rayDirectionLeft * rayDistance, Color.green);

            rightRayHitWall = rightRayHitWall || Physics.Raycast(rayRight, rayDistance, hitLayers);
            leftRayHitWall = leftRayHitWall || Physics.Raycast(rayLeft, rayDistance, hitLayers);
            
        }

        isWallRunning = (rightRayHitWall || leftRayHitWall) && !player.isGrounded;

        playerAnimatorControler.SetBool("wallRunRight", rightRayHitWall);
        playerAnimatorControler.SetBool("wallRunLeft", leftRayHitWall);


        if (isWallRunning)
        {
            doubleJump = false; 
            playerAnimatorControler.SetBool("jump",false);
            playerAnimatorControler.SetInteger("jumpCount",0);

            movePlayer.z = 0;   
            horizontalMove = 0f;
            player.transform.rotation = Quaternion.LookRotation(player.transform.forward);
            movePlayer.z = 0;
            gravity = wallRuningGravity;
            playerSpeed = wallRunSpeed;
            wallDirection = Vector3.zero;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, hitLayers))
            {
                wallDirection = -hit.normal;
            }
            wallForce = wallDirection * wallRuningForce;
            player.Move(wallForce * Time.deltaTime);
            
            if (actionMan.actions["Jump"].triggered)
                {
                    Debug.Log("salto en muro");
                    Vector3 jumpDirection = -wallDirection.normalized;
                    movePlayer = jumpDirection * jumpForce;
                }
        }
        if (wasWallRunning && !isWallRunning)
        {
            movePlayer = Vector3.zero;
        }
    }


    public void JumpingPlayer(){
        if (player.isGrounded)
        {
            doubleJump = false;
            playerAnimatorControler.SetBool("jump",false);
            playerAnimatorControler.SetInteger("jumpCount",0);
        }

        if (actionMan.actions["Jump"].triggered)
        {
            playerAnimatorControler.SetBool("jump",true);
            playerAnimatorControler.SetInteger("jumpCount", playerAnimatorControler.GetInteger("jumpCount") + 1);
            playerAnimatorControler.SetFloat("verticalForce",fallVelocity);
            if (player.isGrounded || !doubleJump)
            {
                fallVelocity = jumpForce;
                movePlayer.y = fallVelocity;
                doubleJump = !player.isGrounded;
            }
        }
    }

    public void SetGravity()
    {
        if (player.isGrounded)
        {
            gravity = originalGravity;
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else 
        {
            if (!isWallRunning)
            {
                fallVelocity -= gravity * Time.deltaTime;
                movePlayer.y = fallVelocity;
            }
        }
        playerAnimatorControler.SetBool("isOnGround",player.isGrounded);
        playerAnimatorControler.SetFloat("verticalForce",fallVelocity);
        SlideDown();
    }

    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
        if (isOnSlope)
        {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;
            movePlayer.y += slopeForceDown;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
        if (hitNormal.y < 0.1f)
        {
            isOnSlope = true;
        }
        else 
        {
            isOnSlope = false;
        }

        if (!player.isGrounded && hit.normal.y < 0.1f)
        {
            if (actionMan.actions["Jump"].triggered)
            {
                Debug.DrawRay(hit.point,hit.normal,Color.red,1.25f);
                fallVelocity = jumpForce * wallJumpJumpForce;
            }
        }
    }
}