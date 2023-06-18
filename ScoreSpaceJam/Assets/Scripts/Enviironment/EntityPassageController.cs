using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EntityPassageController : MonoBehaviour
{
    private enum PassageType
    {
        CIRCLE,
        SPHERE
    }

    [SerializeField] private PassageEntity _EntityPrefab;
    [SerializeField] private float _EntitySpeed;
    [Space]
    [SerializeField] private PassageType _PassageType = PassageType.SPHERE;
    [SerializeField] private float _Radius;

    [SerializeField] private float _MaxPassageAngle = 15;

    [SerializeField] private float _SpawnDelay = 1;


    [SerializeField] private SphereCollider _collider;
    private float _spawnDelayTimer = 0;

    void Awake()
    {
        if(_collider == null)
            _collider = this.GetComponent<SphereCollider>();
    }

    void Update()
    {      
        if(_spawnDelayTimer >= _SpawnDelay) {
            Random.InitState(System.DateTime.Now.Millisecond);
            SpawnEntity();
            _spawnDelayTimer = 0;
        }
        
        _spawnDelayTimer += Time.deltaTime;
    }

    private void OnValidate()
    {
        UpdateCollider();
    }    

    private void UpdateCollider()
    {
        if(_collider.radius != _Radius) {
            _collider.radius = _Radius;
        }
    }

    void SpawnEntity()
    {
        //Select position on the "edge" of ENPC (Random sphere point * _Radius)

        Vector3 randomPoint = Vector3.zero;
        Vector3 forwardDir = this.transform.forward;
        Quaternion forwardRotation = Quaternion.identity;

        switch(_PassageType)
        {
            case PassageType.SPHERE:
                randomPoint = Random.onUnitSphere.normalized * _Radius;

                forwardDir = this.transform.position - (randomPoint + this.transform.position);
                forwardRotation = Quaternion.LookRotation(forwardDir);

                if(_MaxPassageAngle > 0) {
                    float randomAngleDegrees = Random.Range(0, _MaxPassageAngle);
                    forwardRotation *= Quaternion.Euler(Vector3.up * randomAngleDegrees);
                    forwardRotation *= Quaternion.Euler(Vector3.right * randomAngleDegrees);
                }

            break;

            case PassageType.CIRCLE:
                Vector2 circleUnit = Random.insideUnitCircle.normalized;
                randomPoint = new Vector3(circleUnit.x, 0, circleUnit.y) * _Radius;

                forwardDir = this.transform.position - (randomPoint + this.transform.position);
                forwardRotation = Quaternion.LookRotation(forwardDir);

                if(_MaxPassageAngle > 0) {
                    float randomAngleDegrees = Random.Range(0, _MaxPassageAngle);
                    forwardRotation *= Quaternion.Euler(Vector3.up * randomAngleDegrees);
                    //forwardRotation *= Quaternion.Euler(Vector3.right * randomAngleDegrees);
                }
                
            break;
        }

        //Spawn Entity on that point
        PassageEntity entity = GameObject.Instantiate(_EntityPrefab, this.transform.position + randomPoint, forwardRotation);
        entity.BeginPassage();

        AdvancedDamageZoneController dmgCntrl = entity.GetComponent<AdvancedDamageZoneController>();
        if(dmgCntrl != null){
            dmgCntrl.Init(_EntitySpeed);
        }
        
    }

    public void SetSpawnDelay(float value)
    {
        _SpawnDelay = value;
    }

    public void SetEntitySpeed(float value)
    {
        _EntitySpeed = value;
    }


    void OnTriggerEnter(Collider col)
    {

    }

    void OnTriggerStay(Collider col)
    {
        
    }

    void OnTriggerExit(Collider col)
    {
        PassageEntity entity = col.GetComponent<PassageEntity>();
        if(entity != null) {
            entity.Destroy();
        }
    }

}
