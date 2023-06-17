using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _DamageZonePrefab;
    [Space]
    [SerializeField] private Transform[] _SpawnTransform;

    [Space] [Header("Spawner Settings (Scaling with Score)")]
    [Tooltip("Time delay between spawn calls")]
    [SerializeField] private float _NextSpawnTime; 



    private float _nextSpawnTimer;



    void Update()
    {
        if(_nextSpawnTimer >= _NextSpawnTime) 
        {
            
        }

        _nextSpawnTimer += Time.deltaTime;
    }

    public void SpawnNextZone()
    {

    }


    public void SpawnDamageZone(GameObject prefab, Transform spawnOrigin, Vector2 offset, float movementSpeed)
    {
        GameObject dmgZone = GameObject.Instantiate(_DamageZonePrefab, spawnOrigin.transform.position, spawnOrigin.transform.rotation, spawnOrigin);

    }
}
