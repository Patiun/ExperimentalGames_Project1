using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMowerSpawner : MonoBehaviour {

    public float minDelay, maxDelay;
    public float xRange, zRange;
    public bool canSpawn;

    private GameObject lawnMower;
    private float nextSpawnTime;
    private ObjectPooler pooler;

	// Use this for initialization
	void Start () {
        pooler = ObjectPooler._sharedInstance;
        nextSpawnTime = Time.time + Random.Range(minDelay, maxDelay);
        canSpawn = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (canSpawn)
        {
            if (Time.time > nextSpawnTime)
            {
                SpawnLawnMower();
            }
        } else
        {
            if (lawnMower == null || !lawnMower.activeInHierarchy)
            {
                canSpawn = true;
                nextSpawnTime = Time.time + Random.Range(minDelay, maxDelay);
            }
        }
        
	}

    public void SpawnLawnMower()
    {
        canSpawn = false;
        Vector3 pos = transform.position;
        pos.x += Random.Range(-xRange, xRange);
        pos.z += Random.Range(-zRange, zRange);
        lawnMower = pooler.SpawnFromPool("LawnMower", pos, Quaternion.identity);
    }
}
