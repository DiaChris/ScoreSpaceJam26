using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassageEntity : MonoBehaviour
{
    [SerializeField] private UnityEvent _BeginPassage;
    [Space]
    [SerializeField] private float _Speed;

    
    public void BeginPassage()
    {
        _BeginPassage.Invoke();

        StartMovement();
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }


    //Movement logic - potencially separate script
    #region EntityMovement
    void StartMovement()
    {
        this.GetComponent<Rigidbody>().velocity = this.transform.forward * _Speed;
    }
    #endregion
}
