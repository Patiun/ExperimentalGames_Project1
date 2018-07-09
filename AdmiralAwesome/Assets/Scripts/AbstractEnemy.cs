using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractEnemy : MonoBehaviour {

    public bool canDropPowerup;
    public List<string> droppableObjects;
    public float dropRate;
    public Vector3 p1, p2, p3, p4;
    private GameObject g1, g2, g3, g4;
    protected SpriteRenderer spriteRenderer;
    protected NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	protected void Update () {
        GeneratePoints();
        Debug.DrawLine(transform.position, p1);
        Debug.DrawLine(transform.position, p2);
        Debug.DrawLine(transform.position, p3);
        Debug.DrawLine(transform.position, p4);

        agent.SetDestination(Camera.main.transform.position);
        transform.LookAt(Camera.main.transform.position);
    }

    protected void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 max = spriteRenderer.bounds.max;
        Vector3 min = spriteRenderer.bounds.min;
        float middleZ = (max.z + min.z) / 2;
        p1 = new Vector3(max.x, max.y, middleZ);
        p2 = new Vector3(max.x, min.y, middleZ);
        p3 = new Vector3(min.x, max.y, middleZ);
        p4 = new Vector3(min.x, min.y, middleZ);
        g1 = new GameObject("g1");
        g1.transform.parent = transform;
        g1.transform.position = p1;
        g2 = new GameObject("g2");
        g2.transform.parent = transform;
        g2.transform.position = p2;
        g3 = new GameObject("g3");
        g3.transform.parent = transform;
        g3.transform.position = p3;
        g4 = new GameObject("g4");
        g4.transform.parent = transform;
        g4.transform.position = p4;

        agent.Warp(transform.position);
        agent.SetDestination(Camera.main.transform.position);
    }

    private void GeneratePoints()
    {
        p1 = g1.transform.position;
        p2 = g2.transform.position;
        p3 = g3.transform.position;
        p4 = g4.transform.position;
        /*
        p1 = Camera.main.WorldToScreenPoint(g1.transform.position);
        p1.z = distFromCamera;
        p2 = Camera.main.WorldToScreenPoint(g2.transform.position);
        p2.z = distFromCamera;
        p3 = Camera.main.WorldToScreenPoint(g3.transform.position);
        p3.z = distFromCamera;
        p4 = Camera.main.WorldToScreenPoint(g4.transform.position);
        p4.z = distFromCamera;
        */
    }
    
    protected void DropPowerup()
    {
        if (canDropPowerup)
        {
            if (Random.Range(0,100f) < dropRate)
            {
                int i = Random.Range(0, droppableObjects.Count);
                GameObject powerUp = ObjectPooler._sharedInstance.SpawnFromPool(droppableObjects[i], transform.position, Quaternion.identity);
            }
        }
    }
}
