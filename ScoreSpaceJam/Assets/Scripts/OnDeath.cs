using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDeath : MonoBehaviour
{
    [SerializeField] private UnityEvent _OnDeathEvent;
    [SerializeField] private ParticleSystem shootFood;
    [SerializeField] private AudioSource deathSound;
    
    public void DestroyPlayer()
    {
       
        Destroy(gameObject);
    }

    public void UnlivePlayer()
    {
        deathSound.Play();
        Instantiate(shootFood);
        
        _OnDeathEvent.Invoke();
    }

}
