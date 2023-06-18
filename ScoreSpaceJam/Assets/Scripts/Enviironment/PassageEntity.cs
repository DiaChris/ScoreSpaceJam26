using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassageEntity : MonoBehaviour
{
    [SerializeField] private UnityEvent _BeginPassage;
    [Space]
    [SerializeField] private float _Speed;

    
    public void SetSpeed(float speed)
    {
        _Speed = speed;
    }

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
        //this.GetComponent<Rigidbody>().velocity = this.transform.forward * _Speed;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = Vector3.MoveTowards (this.transform.position, this.transform.position + this.transform.forward, Time.deltaTime * _Speed);
        this.GetComponent<Rigidbody>().MovePosition(newPosition);
    }
    #endregion
}
