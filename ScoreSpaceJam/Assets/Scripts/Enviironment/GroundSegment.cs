using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GroundSegmentPosition {
        TOP,
        MID,
        BOT
        //JUNGLE XDDDDD ....... -_- why am i like this ... -krajst
    }

public class GroundSegment : MonoBehaviour
{
    


    [SerializeField] private GroundSegmentPosition _CurrentPosition = GroundSegmentPosition.MID;

    [SerializeField] private float _UpperPositionLimit = 5;
    [SerializeField] private float _LowerPositionLimit = -5;

    [Space]
    [SerializeField] private float _MoveSpeed = .1f;

    public bool ActiveDebug = false;
    

    private Vector3 _defaultPosition;
    private Rigidbody _rb;
    private Coroutine _moveCoroutine;


    void Awake()
    {
        _rb = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
        _defaultPosition = this.transform.position;
    }

    void Update()
    {

        /*

        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Start up");
            MoveToPosition(GroundSegmentPosition.TOP);
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            MoveToPosition(GroundSegmentPosition.MID);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Start down");
            MoveToPosition(GroundSegmentPosition.BOT);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
           StopAllCoroutines();
        }
        */

    }

    public void MoveToPosition(GroundSegmentPosition position)
    {
        if(_moveCoroutine != null) {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }

        Vector3 targetPosition = this.transform.position;

        switch(position)
        {
            case GroundSegmentPosition.TOP:
                targetPosition.y = _defaultPosition.y + _UpperPositionLimit;
            break;


            case GroundSegmentPosition.MID:
                targetPosition.y = _defaultPosition.y;
            break;


            case GroundSegmentPosition.BOT:
                targetPosition.y = _defaultPosition.y + _LowerPositionLimit;
            break;
        }

        _moveCoroutine = StartCoroutine(MoveCo(targetPosition));
    }
    

    
    private IEnumerator MoveCo(Vector3 targetPosition)
    {
        while(Vector3.Distance(this.transform.position, targetPosition) > 0.05f)
        {           
            //_rb.MovePosition(_defaultPosition + targetPosition * _MoveSpeed * Time.deltaTime);
            this.transform.position = Vector3.MoveTowards (this.transform.position, targetPosition, Time.deltaTime * _MoveSpeed);
            
            yield return new WaitForEndOfFrame();

        }
    }

    public GroundSegmentPosition GetCurrentPosition()
    {
        return _CurrentPosition;
    }

    public void MoveToRandomPosition()
    {
        MoveToPosition(GetRandomPositon(_CurrentPosition));   
    }

    public GroundSegmentPosition GetRandomPositon(GroundSegmentPosition exclude)
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        List<GroundSegmentPosition> positions = new List<GroundSegmentPosition>() {
            GroundSegmentPosition.TOP,
            GroundSegmentPosition.MID,
            GroundSegmentPosition.BOT
        };

        positions.Remove(exclude);

        int randomNum = Random.Range(0, positions.Count);


        return positions[randomNum];
    }
}
