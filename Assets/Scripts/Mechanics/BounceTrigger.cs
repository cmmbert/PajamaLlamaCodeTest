using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTrigger : MonoBehaviour
{
    public float BounceStrength = 5;
    public bool OnlyBounceWhileGrounded = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var kin = collision.GetComponent<KinematicObject>();
        if(kin)
        {
            if(!OnlyBounceWhileGrounded || kin.IsGrounded) 
                kin.Bounce(BounceStrength);
        }
    }
}
