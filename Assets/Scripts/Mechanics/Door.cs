using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<BaseCondition> Conditions;
    public Transform ClosedPos;
    public Transform OpenPos;
    public float TimeToOpen;
    public float OpenSpeedMultiplier = 2;
    public float CloseSpeedMultiplier = 1;

    private Vector3 _startPos;
    private float _timeOpen;
    private float _direction;

    private void Start()
    {
        _startPos = OpenPos.position; 
    }
    void Update()
    {
        CalculateDirection();

        _timeOpen = Mathf.Clamp(_timeOpen + Time.deltaTime * _direction, 0, TimeToOpen);
        var percCompleted = _timeOpen / TimeToOpen;
        transform.position = _startPos + (ClosedPos.position - OpenPos.position) * percCompleted;
    }

    private void CalculateDirection()
    {
        _direction = 1 * OpenSpeedMultiplier;
        foreach (var cond in Conditions)
        {
            if (cond.Condition == false)
            {
                _direction = -1 * CloseSpeedMultiplier;
                break;
            }
        }
    }
}
