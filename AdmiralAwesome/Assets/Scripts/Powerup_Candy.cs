using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Candy : AbstractPowerUp, ICollectable, IPooledObject
{
    public PlayerController pc;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public void Pickup()
    {
        pc.SpeedBoost();
        gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        
    }
}
