using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] Transform targetTransform;
    [SerializeField] float Strenght;
    [SerializeField] Transform explosionPosition;
    [SerializeField] float jumpHeight;
    [SerializeField] float groundCheckHeight;
    [SerializeField] Animator jumpAn;
    [SerializeField] ParticleSystem conffetiParticles;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] TMP_Text TimeCount;
    [SerializeField] TMP_Text Finish;
    [SerializeField] TMP_Text GameOver;
    public LayerMask floor;
    private bool inAir = false;
    private bool canPlayHitSound = true;
    public bool bigDoubleJump;
    private bool doubleJump;
    private Rigidbody Player;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    private bool stopTime = false;
    private bool CanJump = true;
    private float startTime = 0;
    //private MoveOpener moveOpener;
    //private CameraFollow setCamera;
    private Vector3 safeLocation;
    //private PlayerWeaponManager playerWeaponManager;
    //private GunAttack gunAttack;
    //TODO add support for controllers (monkey code vid)
    void Awake()
    {
        Application.targetFrameRate = 30;
        Player = GetComponent<Rigidbody>();
        //moveOpener = GetComponentInChildren<MoveOpener>();
        //setCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        //playerWeaponManager = GetComponent<PlayerWeaponManager>();
        //gunAttack =  GetComponent<GunAttack>();
        //playerInput = GetComponent<PlayerInput>();
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && (!inAir || doubleJump))
        {
            JumpUp();
            doubleJump = false;
        }
    }
    private void JumpUp()
    {
        Player.AddForce(Vector3.up * 15f, ForceMode.Impulse);
        doubleJump = false;
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Player.velocity = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Player.angularVelocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(0, 6, -13);
            startTime = Mathf.Round(Time.time);
            //Finish.enabled = false;
            stopTime = false;
            //GameOver.enabled = false;
        }
        GroundCheck();
        Respawn();
        if (!stopTime)
        {
            TimeCount.text = (Mathf.Round(Time.time) - startTime).ToString();
        }
    }
    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckHeight, floor))
        {
            inAir = false;
            safeLocation = transform.position;
            if (bigDoubleJump)
            {
                doubleJump = true;
            }
        }
        else
            inAir = true;
    }
    
    private void Respawn()
    {
        if (transform.position.y <= -30)
        {
            Player.velocity = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            Player.angularVelocity = new Vector3(0, 0, 0);
            transform.position = safeLocation;
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "floor")
    //        inAir = false;
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "floor")
    //        inAir = true;
    //}
    /// <summary>
    /// Enable player controls
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (canPlayHitSound)
        {
            hitSound.Play();
            canPlayHitSound = false;
            StartCoroutine(CooldownForSound());
        }
    }
    private IEnumerator CooldownForSound()
    {
        yield return new WaitForSeconds(.3f);
        canPlayHitSound = true;
    }
    private void OnEnable()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Gameplay.Enable();
        playerInputActions.Gameplay.Jump.performed += Jump;
        //playerInputActions.Gameplay.Attack.performed += Attack;
        //playerInputActions.Gameplay.Attack.canceled += AttackCanceled;
    }
    private void OnDisable()
    {
        playerInputActions.Gameplay.Disable();
    }

    private void PlayerMovement()
    {
        if (playerInputActions != null)
        {
            Vector2 inputVector = playerInputActions.Gameplay.Movement.ReadValue<Vector2>().normalized;
            Quaternion rotation = Quaternion.Euler(0, 45, 0);
            if (Player.angularVelocity.magnitude < playerSpeed)
            {
                Player.AddTorque(rotation * new Vector3(inputVector.y, 0, -inputVector.x) * playerSpeed * Time.deltaTime * 100f, ForceMode.Force);
            }
            else
            {
                Vector3 playerVelocity = new Vector3(Player.angularVelocity.x, Player.angularVelocity.y, Player.angularVelocity.z);
                playerVelocity = playerVelocity.normalized * playerSpeed;
                playerVelocity = playerVelocity.normalized * playerSpeed;
                Player.angularVelocity = playerVelocity;
            }
            //Debug.Log(inAir);
            if (!inAir)
                Player.AddForce(rotation * new Vector3(inputVector.x, 0, inputVector.y) * playerSpeed * Time.deltaTime * 100f, ForceMode.Force);
            else
                Player.AddForce(rotation * new Vector3(inputVector.x, 0, inputVector.y) * playerSpeed/2 * Time.deltaTime * 100f, ForceMode.Force);
            if(Player.velocity.magnitude > playerSpeed)
            {
                Vector3 playerVelocity = new Vector3(Player.velocity.x,0, Player.velocity.z);
                playerVelocity = playerVelocity.normalized * playerSpeed;
                playerVelocity = playerVelocity.normalized * playerSpeed;
                Player.velocity = new Vector3(playerVelocity.x,Player.velocity.y,playerVelocity.z);
            }
            //setCamera.setLocation(inputVector);
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (!inAir && CanJump)
        {
            CanJump = false;
            Instantiate(conffetiParticles);
            Player.AddForce(-(explosionPosition.position-transform.position) * jumpHeight, ForceMode.Force);
            //Player.AddExplosionForce(jumpHeight, explosionPosition.position, 10);
            jumpAn.Play("Jump");
            StartCoroutine(JumpCooldown());
            jumpSound.Play();
        }

    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        CanJump = true;
    }


}
