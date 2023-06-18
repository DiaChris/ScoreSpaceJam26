using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 6.0f;
    [SerializeField] Transform targetTransform;
    [SerializeField] float Strenght;
    [SerializeField] Transform explosionPosition;
    [SerializeField] float jumpHeight;
    [SerializeField] float groundCheckHeight;
    [SerializeField] Animator jumpAn;
    [SerializeField] ParticleSystem conffetiParticles;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource hitSound;
    [SerializeField] TMP_Text TimeCount;
    [SerializeField] TMP_Text Finish;
    [SerializeField] TMP_Text GameOver;
    public LayerMask floor;
    private bool inAir = false;
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
    private void OnCollisionEnter(Collision collision)
    {
        hitSound.Play();
        if (collision.gameObject.tag == "finish")
        {
            if (float.Parse(TimeCount.text) <= 180)
            {
                Finish.enabled = true;
                jumpSound.Play();
                Instantiate(conffetiParticles);
                Instantiate(conffetiParticles);
                Instantiate(conffetiParticles);
                Instantiate(conffetiParticles);
                Instantiate(conffetiParticles);
                Instantiate(conffetiParticles);
                Instantiate(conffetiParticles);

            }
            else
                GameOver.enabled = true;
            stopTime = true;
        }
        if (collision.gameObject.CompareTag("DamageZone"))
        {
            GetComponent<Health>().Damage(1);
            // Later we can put the next line in
            //GetComponent<Health>().Damage(collision.gameObject.GetComponent<damagevaluescript>().damage);
            Destroy(collision.gameObject);

        }
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
            Vector2 inputVector = playerInputActions.Gameplay.Movement.ReadValue<Vector2>();
            Player.AddTorque(new Vector3(inputVector.y, 0, -inputVector.x) * playerSpeed * Time.deltaTime, ForceMode.Force);
            //Debug.Log(inAir);
            if(!inAir)
                Player.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * playerSpeed * Time.deltaTime, ForceMode.Force);
            else
                Player.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * playerSpeed/2 * Time.deltaTime, ForceMode.Force);
            //setCamera.setLocation(inputVector);
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        //moveOpener.enabled = true;
        //Player.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        if (!inAir && CanJump)
        {
            CanJump = false;
            Instantiate(conffetiParticles);
            Player.AddExplosionForce(jumpHeight, explosionPosition.position, 10);
            jumpAn.Play("Jump");
            StartCoroutine(JumpCooldown());
            jumpSound.Play();
        }

    }

    //private void Attack(InputAction.CallbackContext context)
    //{
    //    playerWeaponManager.AttackWithWeapon();
    //}
    //private void AttackCanceled(InputAction.CallbackContext context)
    //{
    //    gunAttack.CancelAttack();
    //}
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - groundCheckHeight, this.transform.position.z));
    //}
    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        CanJump = true;
    }


}
