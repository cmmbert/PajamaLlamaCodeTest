using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float CurrentAngle = 0;

    public bool StraightMovement;

    public float SinOffset = 2;
    public float CosOffset = 1;

    public Transform StartPnt;
    public Transform EndPnt;

    public float TimeToReach = 3;
    public float percTravelled;

    // Update is called once per frame
    void Update()
    {
        percTravelled = Mathf.PingPong(Time.time, TimeToReach) / TimeToReach;
        StraightMov();

        if (!StraightMovement)
            CosSinMov();
    }

    public void StraightMov()
    {
        var dir = EndPnt.position - StartPnt.position;
        transform.position = StartPnt.position + dir * percTravelled;
    }

    public void CosSinMov()
    {
        Vector3 newPos = new();
        newPos.x = Mathf.Cos(percTravelled * Mathf.PI * CosOffset);
        newPos.y = Mathf.Sin(percTravelled * Mathf.PI * SinOffset);
        transform.position += newPos;
    }
}
