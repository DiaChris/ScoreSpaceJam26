using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Quaternion rotation;
    [SerializeField]
    private float moveSmoothness;
    [SerializeField]
    private float cameraSmoothness;
    private Transform CameraRotationPoint;
    private Vector3 _currentVelocity;
    private Quaternion cameraRotation;
    private Vector3 cameraPosition;
    private Ray ray;
    private RaycastHit hitInfo;
    //private void Awake()
    //{
    //    CameraRotationPoint=GetComponentInParent<Transform>();
    //}
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        //Quaternion desiredRotation = this.gameObject.
        this.transform.position = Vector3.SmoothDamp(this.transform.position,desiredPosition,ref _currentVelocity,moveSmoothness);
        //this.transform.rotation = rotation;
        //if(cameraRotation== null)
        //    this.transform.rotation = rotation;
        //else
        // CameraRotationPoint.rotation = Quaternion.Lerp(CameraRotationPoint.rotation, cameraRotation, cameraSmoothness);
        transform.rotation = rotation;/*Quaternion.Lerp(transform.rotation, cameraRotation, cameraSmoothness);*/
        //this.transform.LookAt(target);
        //transform.Translate(Vector3.right * Time.deltaTime);
        //this.transform.RotateAround(target.position, Vector3.up, cameraRotation.eulerAngles.y*Time.deltaTime);
        //ray.origin = transform.position;
        //ray.direction = Quaternion.LookRotation(target.position).eulerAngles;
        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    if (hitInfo.collider.gameObject.tag != "floor")
        //    {
        //        //hitInfo.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
        //        StartCoroutine(Appear(hitInfo.collider.gameObject.GetComponent<MeshRenderer>()));
        //    }
        //}

    }
    //private IEnumerator Appear(MeshRenderer mesh)
    //{
    //    yield return new WaitForSeconds(4f);
    //    //mesh.enabled = true;
    //}
    //public void setLocation(Vector3 velocity)
    //{
    //    //cameraPosition = velocity;
    //    if (velocity.x != 0 || velocity.y != 0)
    //    {
    //        Vector3 velocity2 = new Vector3(velocity.x, 0, velocity.y);
    //        cameraRotation = Quaternion.LookRotation(velocity2) * Quaternion.Euler(new Vector3(30, 0, 0));
    //    }
    //}
}
///ROTATING WITH A and D until pressed
///transform.Rotate(new Vector3(cameraPosition.y, cameraPosition.x, 0f));