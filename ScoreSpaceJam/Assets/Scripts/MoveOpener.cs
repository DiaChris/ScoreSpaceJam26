using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOpener : MonoBehaviour
{
    [SerializeField]
    private float Strenght;
    [SerializeField]
    private float Height;
    [SerializeField]
    private float JumpTime;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timePassed = 0f;
    private bool FinishedAn = false;
    //private void Awake()
    //{
    //    startPosition = transform.position;
    //    targetPosition = startPosition + transform.up * Strenght;
    //}
    private void OnEnable()
    {
        //startPosition = transform.position;
        //targetPosition = transform.position + transform.forward * Height;
        //FinishedAn = false;
        //timePassed = 0;
    }
    private void Update()
    {
        //timePassed += Time.deltaTime;
        //if (timePassed >= JumpTime)
        //{
        //    targetPosition = startPosition;
        //    startPosition = transform.position;
        //    timePassed = 0;
        //    if (FinishedAn)
        //        this.GetComponent<MoveOpener>().enabled = false;
        //    FinishedAn = true;
        //}
            
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Strenght * Time.deltaTime).normalized;
    }
}
