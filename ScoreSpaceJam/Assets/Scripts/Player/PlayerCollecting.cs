using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollecting : MonoBehaviour
{
    [SerializeField] private AudioSource collectSound;
    public float score;
    public LevelManager LevelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Score: " + score);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Score"))
        {
            collectSound.Play();
            score += collision.gameObject.GetComponent<CoinsScript>().points;
            LevelManager.UpdateScore(score);

            if (collision.gameObject.GetComponent<CoinsScript>().givesdoublejump)
            {
                GetComponent<PlayerController>().bigDoubleJump = true;
            }


            Destroy(collision.gameObject);

        }
    }
}
