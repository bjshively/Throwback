using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : Weapon
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
            return .4f;
        } 
    }

    void Start()
    {
        base.Start();
        pc.canFire = false;

        // Pistol fire limited to once/second
        // We may want bullets to last longer than the delay between shots
        Invoke("SelfDestruct", 3);
        Invoke("ResetFire", fireDelay);
    }
}
