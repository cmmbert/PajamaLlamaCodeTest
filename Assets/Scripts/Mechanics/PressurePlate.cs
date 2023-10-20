using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressurePlate : BaseCondition
{
    public float SinkDistance = 0.15f;
    public float TimePressedNeeded = 0.1f;

    private float _timePressed = 0;
    private bool _isBeingPressed;
    private Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.position;
    }
    private void Update()
    {
        if(!_isBeingPressed)
            _timePressed -= Time.deltaTime;
        _timePressed = Mathf.Clamp(_timePressed, 0, TimePressedNeeded);
        transform.position = new Vector3(_startPos.x, _startPos.y - (_timePressed / TimePressedNeeded * SinkDistance), _startPos.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _isBeingPressed = true;

        if (_timePressed >= TimePressedNeeded)
        {
            _timePressed = TimePressedNeeded;
            Condition = true;
        }
        else
        {
            _timePressed += Time.deltaTime;
            Condition = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isBeingPressed = false;
        Condition = false;
    }
}
