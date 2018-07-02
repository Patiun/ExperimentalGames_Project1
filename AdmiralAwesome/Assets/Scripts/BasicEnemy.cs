using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : AbstractEnemy, iSquashable{

	// Use this for initialization
	void Start () {
        base.Init();
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
	}

    public bool CanSquash()
    {
        SquishFinder sf = SquishFinder._sharedInstance;
        Debug.Log((sf.CheckPoint(p1) + "," + sf.CheckPoint(p2) + "," + sf.CheckPoint(p3) + "," + sf.CheckPoint(p4)));
        return (sf.CheckPoint(p1)&&sf.CheckPoint(p2)&&sf.CheckPoint(p3)&&sf.CheckPoint(p4));
    }

    public void Squash()
    {
        Debug.Log("Got Squashed");
    }
}
