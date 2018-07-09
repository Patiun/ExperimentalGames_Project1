using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipartEnemy : AbstractEnemy, IPooledObject {

    public List<GameObject> parts;
    public int scoreValue;

    private ObjectPooler pooler;
    private GameController game;
    // Use this for initialization
    void Start () {
        base.Init();
        pooler = ObjectPooler._sharedInstance;
        game = GameController._sharedInstance;
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        if (CheckParts())
        {
            Die();
        }
	}

    private bool CheckParts()
    {
        foreach(GameObject p in parts)
        {
            if (p.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    private void Die()
    {
        if (game == null)
        {
            game = GameController._sharedInstance;
        }
        game.AddScore(scoreValue);
        pooler.SpawnFromPool("Explosion", transform.position + transform.forward, Quaternion.identity);
        AudioManager.instance.Play("Explosion");
        gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        foreach (GameObject p in parts)
        {
            p.SetActive(true);
        }
    }
}
