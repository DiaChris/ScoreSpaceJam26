using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRotation : MonoBehaviour
{
    [SerializeField] private bool _Active = true;
    [SerializeField] private Vector3 _RotationSpeeds;

    void Update()
    {
        if(_Active)
            this.transform.Rotate(_RotationSpeeds.x * Time.deltaTime, _RotationSpeeds.y * Time.deltaTime, _RotationSpeeds.z * Time.deltaTime);
    }
}
