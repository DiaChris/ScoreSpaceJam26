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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCharacter").transform; 
        coefficient = 1;
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
}
