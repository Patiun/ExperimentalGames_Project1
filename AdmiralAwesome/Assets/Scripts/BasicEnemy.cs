using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : AbstractEnemy, iSquashable, IPooledObject{

    public bool allowedToShoot;
    public float fireRate;
    public LayerMask layerMask;
    public float fireRange;
    public int scoreValue;

    private float nextFireTime;
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
        if (allowedToShoot)
        {
            if (agent.remainingDistance <= fireRange)
            {
                agent.isStopped = true;
            } else
            {
                agent.isStopped = false;
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward,out hit, Mathf.Infinity,layerMask.value))
            {
                if (hit.collider.tag == "Player")
                {
                    if (Time.time > nextFireTime)
                    {
                        Fire();
                    }
                }
            }
        }
	}

    public bool CanSquash()
    {
        SquishFinder sf = SquishFinder._sharedInstance;
        Debug.Log((sf.CheckPoint(p1) + "," + sf.CheckPoint(p2) + "," + sf.CheckPoint(p3) + "," + sf.CheckPoint(p4)));
        return (sf.CheckPoint(p1)&&sf.CheckPoint(p2)&&sf.CheckPoint(p3)&&sf.CheckPoint(p4));
    }

    public void Squash()
    {
        game.AddScore(scoreValue);
        gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        base.Init();
        nextFireTime = Time.time;
    }

    private void Fire()
    {
        pooler.SpawnFromPool("Bullet", transform.position+transform.forward, Quaternion.identity);
        nextFireTime = Time.time + 1f / fireRate;
    }
}
