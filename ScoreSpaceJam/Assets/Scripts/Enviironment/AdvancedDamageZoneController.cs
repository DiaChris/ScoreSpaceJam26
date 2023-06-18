using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class AdvancedDamageZoneController : MonoBehaviour
{
    [SerializeField] private Transform _UpperDamageZone;
    [SerializeField] private Transform _MidDamageZone;
    [SerializeField] private Transform _LowerDamageZone;

    
    public void Init(float speed)
    {
        EnableRandomDamageZones();

        this.GetComponent<PassageEntity>().SetSpeed(speed);
    }

    void EnableRandomDamageZones()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        int up =  Random.Range(0,10);  // 0 1 2 3 4  5 6 7 8 9

        Random.InitState((int)System.DateTime.Now.Ticks + up);
        int mid = Random.Range(0,10);

        Random.InitState((int)System.DateTime.Now.Ticks + mid);
        int down = Random.Range(0,10);

        bool upEnabled = up > 4 ? true : false;
        bool midEnabled = mid > 4 ? true : false;
        bool downEnabled = down > 4 ? true : false;

       //Debug.Log(up + "-" + upEnabled + " | " + mid + "-" +  midEnabled + " | " + down + "-" +   downEnabled);

        if(!upEnabled && !midEnabled && !downEnabled) 
        {
            midEnabled = true;
        }

        _UpperDamageZone.gameObject.SetActive(upEnabled);
        _MidDamageZone.gameObject.SetActive(midEnabled);
        _LowerDamageZone.gameObject.SetActive(downEnabled);
    }
}
