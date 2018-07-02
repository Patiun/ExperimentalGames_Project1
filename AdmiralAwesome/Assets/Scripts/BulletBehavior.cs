using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour, iSquashable, IPooledObject {

    public float speed;
    public int damage;
    public float timeAllowedToExist;

    private Vector3 p1, p2, p3, p4;
    private GameObject g1, g2, g3, g4;
    private SpriteRenderer spriteRenderer;

    private float timeToDie;

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        GeneratePoints();
        Debug.DrawLine(transform.position, p1);
        Debug.DrawLine(transform.position, p2);
        Debug.DrawLine(transform.position, p3);
        Debug.DrawLine(transform.position, p4);

        if (Time.time > timeToDie)
        {
            gameObject.SetActive(false);
        }
        transform.LookAt(Camera.main.transform);
    }

    public bool CanSquash()
    {
        SquishFinder sf = SquishFinder._sharedInstance;
        //Debug.Log((sf.CheckPoint(p1) + "," + sf.CheckPoint(p2) + "," + sf.CheckPoint(p3) + "," + sf.CheckPoint(p4)));
        return (sf.CheckPoint(p1) && sf.CheckPoint(p2) && sf.CheckPoint(p3) && sf.CheckPoint(p4));
    }

    public void Squash()
    {
        gameObject.SetActive(false);
    }

    private void GeneratePoints()
    {
        p1 = g1.transform.position;
        p2 = g2.transform.position;
        p3 = g3.transform.position;
        p4 = g4.transform.position;
    }

    protected void Init()
    {
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
    }

    public void OnObjectSpawn()
    {
        timeToDie = Time.time + timeAllowedToExist;
        transform.LookAt(Camera.main.transform);
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Init();
    }

    public void OnCollisionEnter(Collision collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.Damage(damage);
        }
        gameObject.SetActive(false);
    }
}
