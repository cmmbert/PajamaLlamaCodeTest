using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float CurrentAngle = 0;

    public bool StraightMovement;

    public float SinOffset = 1;
    public float CosOffset = 1;

    public Transform StartPnt;
    public Transform EndPnt;

    public float TimeToReach = 3;
    public float percTravelled;

    // Update is called once per frame
    void Update()
    {
        if (StraightMovement)
            StraightMov();
        else
            CosSinMov();
    }

    public void StraightMov()
    {
        percTravelled = Mathf.PingPong(Time.time, TimeToReach) / TimeToReach;

        var dir = EndPnt.position - StartPnt.position;
        //Vector3 newPos = dir.normalized * percTravelled;
        transform.position = StartPnt.position + dir * percTravelled;
    }

    public void CosSinMov()
    {

    }
}
