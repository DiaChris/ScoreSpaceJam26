using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDeath : MonoBehaviour
{
    [SerializeField] private UnityEvent _OnDeathEvent;
    

    public void UnlivePlayer()
    {
        _OnDeathEvent.Invoke();
    }

}
