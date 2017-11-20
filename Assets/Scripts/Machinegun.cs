using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : Weapon
{

    public override float projectileSpeed
    {
        get
        {
            return 10;
        } 
    }

    protected override float fireDelay
    {
        get
        {
            return .4f;
        } 
    }
}