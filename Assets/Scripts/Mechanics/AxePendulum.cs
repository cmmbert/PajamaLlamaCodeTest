using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePendulum : MonoBehaviour
{
    public List<BaseCondition> Conditions;

    public float SwingSpeed = 2;
    public float SwingHeight = 90;
    private float _sin;

    private float _timeAlive = 0;

    void FixedUpdate()
    {
        var locked = false;
        foreach (var cond in Conditions)
        {
            if(!cond.Condition)
            {
                locked = true;
                break;
            }
        }

        if (!locked || Mathf.Abs(_sin ) < 0.99f)
        {
            _timeAlive += Time.deltaTime;
            _sin = Mathf.Sin(_timeAlive * SwingSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, _sin * SwingHeight));
        }
    }
}
