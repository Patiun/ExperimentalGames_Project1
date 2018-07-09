using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
        transform.LookAt(Camera.main.transform);
	}
}
