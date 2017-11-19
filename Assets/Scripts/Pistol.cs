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
    

    // Use this for initialization
    void Start()
    {
        base.Start();
        pc.canFire = false;

        // Pistol fire limited to once/second
        // We may want bullets to last longer than the delay between shots
        Invoke("SelfDestruct", 1);
        Invoke("ResetFire", 1);
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }
}
