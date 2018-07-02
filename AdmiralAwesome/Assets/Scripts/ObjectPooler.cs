using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ObjectPooler for maintaining pools of objects to be spawned into the scene. Uses a Singleton model.
/// Author: Greg Kilmer
/// Inspired by Brackeys' video
/// </summary>
public class ObjectPooler : MonoBehaviour {

    #region Singleton
    public static ObjectPooler _sharedInstance;
    private void Awake()
    {
        _sharedInstance = this;
    }
    #endregion

    /// <summary>
    /// Local Class to hold pool information
    /// </summary>
    [System.Serializable]
    public class Pool
    {
        public string tag; //Tag for the pool
        public GameObject prefab; //Prefab to spawn
        public int size; //Number of objects for the pool
    }

    public List<Pool> pools; //List of all of the pools

    private Dictionary<string, Queue<GameObject>> poolDictionary; //Dictionary mapping the tag of a pool to the queue of items for the pool


	// Use this for initialization
	void Start () {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        //Build the pool dictionary from the list of pools
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject nest = new GameObject(pool.tag + " Container");
            nest.transform.parent = transform;
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab,nest.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
	}

    /// <summary>
    /// Spawns an object from the selected pool at the desired position and rotation if the pool exists
    /// </summary>
    /// <param name="tag">Tag of the desired pool</param>
    /// <param name="position">Location to spawn the object</param>
    /// <param name="rotation">Rotation to spawn the object with</param>
    /// <returns>Spawned GameObject from the desired pool</returns>
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

           IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
           if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }

            poolDictionary[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        } else
        {
            Debug.LogWarning("Pool with tag "+tag+" does not exist.");
        }
        return null;
    }

}
