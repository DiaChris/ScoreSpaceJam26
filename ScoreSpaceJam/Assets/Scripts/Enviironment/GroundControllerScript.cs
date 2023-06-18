using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControllerScript : MonoBehaviour
{
    [SerializeField] private GroundSegment[] _GroundSegments;
    
    private float _groundUpdateDelayTimer = 0;

    [Space] [Header("Difficulty Scaling")]
    [SerializeField] private float _GroundUpdateTime = 1;
    [SerializeField] [Range(0,9)] private int _NumberOfUpdatingPlatforms = 0; //Starts at 0 goes to 9 slowsly

    void Update()
    {
        if(_groundUpdateDelayTimer >= _GroundUpdateTime) {
            Random.InitState(System.DateTime.Now.Millisecond);
            UpdateGround();
            _groundUpdateDelayTimer = 0;
        }
        
        _groundUpdateDelayTimer += Time.deltaTime;
    }

    void UpdateGround()
    {      
        GroundSegment platformToUpdate = GetRandomPlatform();

        platformToUpdate.MoveToRandomPosition();
    }

    public GroundSegment GetRandomPlatform()
    {
        int index = Random.Range(0, 9);

        return _GroundSegments[index];
    }

    public void SetUpdateDelay(float value)
    {
        _GroundUpdateTime = value;
    }
}
