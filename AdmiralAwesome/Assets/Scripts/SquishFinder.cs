using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishFinder : MonoBehaviour {

    public static SquishFinder _sharedInstance;

    public float w, h;
    public Vector3 center;
    public SpriteRenderer marker;
    public float distFromCamera;

    // Use this for initialization
    void Start () {
        _sharedInstance = this;
        center = new Vector3(Screen.width / 2f, Screen.height / 2f, distFromCamera);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool CheckPoint(Vector3 point)
    {
        point = Camera.main.WorldToScreenPoint(point);
        bool xGood = (point.x <= center.x + w/2f && point.x >= center.x - w/2f);
        bool yGood = (point.y <= center.y + h / 2f && point.y >= center.y - h / 2f);
        return (xGood && yGood);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("!");
        if (other.GetComponent<BasicEnemy>().CanSquash())
        {
            Debug.Log("Can Squash");
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BasicEnemy>().CanSquash())
        {
            Debug.Log("Can Squash");
        }
    }
}
