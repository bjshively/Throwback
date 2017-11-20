using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    
    public override float projectileSpeed
    {
        get
        {
            return 5;
        } 
    }

    protected override float fireDelay
    {
        get
        {
            return 1f;
        }
    }
}
