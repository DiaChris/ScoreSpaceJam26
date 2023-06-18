using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class CoinsScript : MonoBehaviour
{
    public float points;
    public bool runningFromPlayer;
    public bool bobbingUpAndDown;
    public bool givesdoublejump;
    public bool healsplayer;
    public bool maxHealth;
    public float speed;
    public int maxUp;
    public int maxDown;
    public Transform player;
    [SerializeField] private float coefficient;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float obstacleDetectionDistance;
    [SerializeField] private float jumpStrenght;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private float groundDetectionDistance;
    private Rigidbody _rigidbody;
    private bool _hasPosition = false;
    private Vector3 _targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCharacter").transform; 
        coefficient = 1;
        _rigidbody = GetComponent<Rigidbody>();
    }

    // We use this method on coin spawning to setup it's variables, behaviour ... etc.
    public void Init(/*Params*/)
    {
        //Set Player
        //Set Type of coin
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -50)
            Destroy(gameObject);
        if (runningFromPlayer)
        {
            Vector3 direction = transform.position - player.transform.position;
            GetComponent<Rigidbody>().AddForce(direction * speed);
            //Debug.Log("running away");
        }
        if (bobbingUpAndDown)
        {
            if (transform.position.y >= maxUp)
            {
                coefficient *= -1;
            }
            if (transform.position.y <= maxDown)
            {
                coefficient *= -1;
            }
            transform.Translate(coefficient * Vector3.right * speed);
        }
    }
    private void FixedUpdate()
    {
        MoveToPosition();
    }
    private void MoveToPosition()
    {


        if (!_hasPosition) {
            _targetPosition = GetPosition();
        }
        Vector2 targetPostionWithoutY = new Vector2(_targetPosition.x, _targetPosition.z);
        Vector2 objectPostionWithoutY = new Vector2(transform.position.x, transform.position.z);
        if (Vector3.Distance(targetPostionWithoutY, objectPostionWithoutY) < 5f) 
            _hasPosition = false;
        Vector3 directionInVector = _targetPosition - this.transform.position;
        directionInVector.y = 0f;
        CheckForObstacle(directionInVector);
        if (_rigidbody.velocity.magnitude < movementSpeed && GroundCheck())
            _rigidbody.AddForce(directionInVector.normalized * movementSpeed * Time.deltaTime * 100f);
        else
        {
            Vector3 coinVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            coinVelocity = coinVelocity.normalized * movementSpeed;
            _rigidbody.velocity = new Vector3(coinVelocity.x, _rigidbody.velocity.y, coinVelocity.z);
        }
    }
    private Vector3 GetPosition()
    {
        _hasPosition = true;
        return new Vector3(Random.Range(-29, 29), 0, Random.Range(-29, 29));
    }
    private void CheckForObstacle(Vector3 direction)
    {
        Debug.DrawRay(transform.position - new Vector3(0, 1f, 0), direction * obstacleDetectionDistance , Color.red);
        if (Physics.Raycast(transform.position - new Vector3(0, 1f,0), direction, obstacleDetectionDistance,floorLayer))
        {
            Jump();
        }
    }
    private void Jump()
    {
        //moveOpener.enabled = true;
        //Player.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        if (GroundCheck())
        {
            _rigidbody.AddForce(Vector3.up * jumpStrenght, ForceMode.Impulse);
        }

    }
    private bool GroundCheck()
    {
        
        if (Physics.Raycast(transform.position, Vector3.down, groundDetectionDistance))
        {
            return true;
        }
        return false;
    }
}
