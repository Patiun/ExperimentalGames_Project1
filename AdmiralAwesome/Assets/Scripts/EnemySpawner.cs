using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [System.Serializable]
    public class EnemySpawnData
    {
        public string poolTag;
        public float weight;
    }

    public bool isSpawning;
    public List<EnemySpawnData> spawnData;
    public float spawnRate;

    private float scale;
    private float[] floors;
    private float nextSpawnTime;
    private ObjectPooler pooler;

	// Use this for initialization
	void Start () {
        pooler = ObjectPooler._sharedInstance;
        nextSpawnTime = Time.time;
		foreach(EnemySpawnData data in spawnData)
        {
            scale += data.weight;
        }
        floors = new float[spawnData.Count];
        for (int i = 0; i < spawnData.Count; i++)
        {
            floors[i] = spawnData[i].weight / scale * 100f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isSpawning)
        {
            if (Time.time >= nextSpawnTime)
            {
                Spawn();
            }
        }
	}

    private void Spawn()
    {
        float value = Random.Range(0, 100f);
        for (int i = 0; i < floors.Length; i++)
        {
            if (value < floors[i])
            {
                GameObject enemy = pooler.SpawnFromPool(spawnData[i].poolTag, transform.position, Quaternion.identity);
                break;
            }
        }
        nextSpawnTime = Time.time + 1f / spawnRate;
    }
}
