﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectionZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Powerup")
        {
            ICollectable collectable = other.gameObject.GetComponent<ICollectable>();
            if (collectable != null)
            {
                collectable.Pickup();
            }
        }
    }
}
