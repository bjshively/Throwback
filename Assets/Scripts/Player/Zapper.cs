﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapper : Weapon
{
    
    public override float projectileSpeed
    {
        get
        {
            return 7;
        } 
    }

    protected override float fireDelay
    {
        get
        {
            return .5f;
        }
    }
}