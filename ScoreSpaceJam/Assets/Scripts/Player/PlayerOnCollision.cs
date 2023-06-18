using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnCollision : MonoBehaviour
{
    [SerializeField] AudioSource hitSound;
    private void OnCollisionEnter(Collision collision)
    {
        hitSound.Play();
    }
}
