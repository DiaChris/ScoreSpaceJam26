using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class DamageZone : MonoBehaviour
{
    [SerializeField] private int _Damage;
    

    void OnTriggerEnter(Collider col)
    {
        IDamagable damagable = col.GetComponent<IDamagable>();
        if(damagable != null) 
        {
            damagable.Damage(_Damage);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        IDamagable damagable = col.collider.GetComponent<IDamagable>();
        if(damagable != null) 
        {
            damagable.Damage(_Damage);
        }
    }
}
