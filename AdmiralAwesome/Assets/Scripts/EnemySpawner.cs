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

    public bool spawnerEnabled;
    public bool isSpawning;
    public List<EnemySpawnData> spawnData;
    public float spawnRate;

    private float scale;
    private float[] floors;
    private float nextSpawnTime;
    private ObjectPooler pooler;
    private GameController game;

	// Use this for initialization
	void Start () {
        pooler = ObjectPooler._sharedInstance;
        game = GameController._sharedInstance;
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
        isSpawning = false;
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
        float total = 0;
        for (int i = 0; i < floors.Length; i++)
        {
            total += floors[i];
            if (value < total)
            {
                GameObject enemy = pooler.SpawnFromPool(spawnData[i].poolTag, transform.position, Quaternion.identity);
                if (game == null)
                {
                    game = GameController._sharedInstance;
                }
                game.AddToWave(enemy);
                break;
            }
        }
        nextSpawnTime = Time.time + 1f / spawnRate;
    }

    public void EndWave()
    {
        isSpawning = false;
    }

    public void StartWave()
    {
        isSpawning = true;
    }
}
