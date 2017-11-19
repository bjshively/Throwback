using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    
    public override float projectileSpeed
    {
        get
        {
            return 15;
        } 
    }

    protected override float fireDelay
    {
        get
        {
            return 1f;
        }
    }

    void Start()
    {
        base.Start();

        // Pistol fire limited to once/second
        // We may want bullets to last longer than the delay between shots
        Invoke("SelfDestruct", 1);
        Invoke("ResetFire", fireDelay);
    }
}
