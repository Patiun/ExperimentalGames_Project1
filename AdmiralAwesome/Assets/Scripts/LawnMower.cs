using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : AbstractEnemy, iSquashable, IPooledObject {

    private ObjectPooler pooler;
    public List<GameObject> enemies;

	// Use this for initialization
	void Start () {
        Init();
        
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
	}

    public bool CanSquash()
    {
        SquishFinder sf = SquishFinder._sharedInstance;
        //Debug.Log((sf.CheckPoint(p1) + "," + sf.CheckPoint(p2) + "," + sf.CheckPoint(p3) + "," + sf.CheckPoint(p4)));
        return (sf.CheckPoint(p1) && sf.CheckPoint(p2) && sf.CheckPoint(p3) && sf.CheckPoint(p4));
    }

    public void Squash()
    {
        pooler.SpawnFromPool("Explosion", transform.position + transform.forward, Quaternion.identity);
        AudioManager.instance.Play("ExplosionLM");
        Detonate();
        gameObject.SetActive(false);
    }

    public void Detonate()
    {
        foreach(GameObject enemy in enemies)
        {
            iSquashable s = enemy.GetComponent<iSquashable>();
            if (s != null)
            {
                s.Squash();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    public void OnObjectSpawn()
    {
        Init();
    }

    public new void Init()
    {
        base.Init();
        canDropPowerup = false;
        if (pooler == null) { pooler = ObjectPooler._sharedInstance; }
        enemies = new List<GameObject>();
    }
}
