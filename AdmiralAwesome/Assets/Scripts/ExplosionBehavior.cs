using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour, IPooledObject {

    public float timeAllowedToExist;

    private float timeToDie;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= timeToDie)
        {
            gameObject.SetActive(false);
        }
        transform.LookAt(Camera.main.transform);
	}

    public void OnObjectSpawn()
    {
        timeToDie = Time.time + timeAllowedToExist;
    }
}
