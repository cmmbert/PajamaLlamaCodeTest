using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestroyedOrDisabled : BaseCondition
{
    public GameObject ObjectToCheck;
    void Update()
    {
        if (ObjectToCheck == null || !ObjectToCheck.activeInHierarchy)
            Condition = true;
        else
            Condition = false;
    }
}
