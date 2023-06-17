using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _CoinPrefab;

    [Space] [Header("Spawner Settings (Scaling with Score)")]
    [Tooltip("Time delay between spawn calls")]
    [SerializeField] private float _NextSpawnTime = 5; 
    [Space] [Header("Random Spawn Settings")]
    [SerializeField] private Vector2 _SpawnAreDimensions = new Vector2(10, 10);
    [SerializeField] private float _SpawnCheckYOffset = 10;
    [SerializeField] private LayerMask _CoinSpawnLayer;

    private float _nextSpawnTimer = 0;

    void Update()
    {
        if(_nextSpawnTimer >= _NextSpawnTime) 
        {
            Debug.Log("Try spawn coin");
            SpawnNextCoin();
            _nextSpawnTimer = 0;
        }

        Random.InitState(System.DateTime.Now.Millisecond);
        _nextSpawnTimer += Time.deltaTime;
    }

    public void SpawnNextCoin()
    {
        float xCoordinate = Random.Range(-_SpawnAreDimensions.x, _SpawnAreDimensions.x);
        float zCoordinate = Random.Range(-_SpawnAreDimensions.y, _SpawnAreDimensions.y);

        Vector3 checkOrigin = new Vector3(xCoordinate, _SpawnCheckYOffset, zCoordinate);
        //if(Physics.Raycast(checkOrigin, Vector3.down, _SpawnCheckYOffset * 2, _CoinSpawnLayer))
        Debug.DrawRay(checkOrigin, Vector3.down, Color.yellow, 5);
        if(Physics.Raycast(checkOrigin, Vector3.down, out RaycastHit hit, _SpawnCheckYOffset * 2, _CoinSpawnLayer))
        {
            SpawnCoin(_CoinPrefab, hit.point + new Vector3(0,_SpawnCheckYOffset,0));
            Debug.Log("spawn coin");
        }
    }


    public void SpawnCoin(GameObject prefab, Vector3 position)
    {
        GameObject coin = GameObject.Instantiate(prefab, position, Quaternion.identity, this.transform);
        //coin.GetComponent<CoinsScript>().Init();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(_SpawnAreDimensions.x * 2, _SpawnCheckYOffset * 2, _SpawnAreDimensions.y * 2));   
    }
}
