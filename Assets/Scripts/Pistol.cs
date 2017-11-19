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

        // Pistol bullets are destroyed after 5 seconds automatically
        Invoke("SelfDestruct", 5);
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }
}
