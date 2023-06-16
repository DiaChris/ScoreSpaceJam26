using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("!!!!!");
        collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    other.GetComponent<MeshRenderer>().enabled = true;
    //}
}
