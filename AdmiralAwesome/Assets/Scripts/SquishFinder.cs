using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquishFinder : MonoBehaviour {

    public static SquishFinder _sharedInstance;

    public float w, h;
    public Vector3 center;
    public SpriteRenderer marker;
    public float distFromCamera;
    public Image squashIndicator;
    public GameObject curTarget;
    public bool canSquish;

    // Use this for initialization
    void Start () {
        _sharedInstance = this;
        center = new Vector3(Screen.width / 2f, Screen.height / 2f, distFromCamera);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Fire1") != 0)
        {
            if (canSquish)
            {
                Squash();
            }
        }
	}

    public void Squash()
    {
        curTarget.GetComponent<iSquashable>().Squash();
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
        //Debug.Log("!");
        iSquashable enemy = other.GetComponent<BasicEnemy>();
        if (enemy != null)
        {
            if (enemy.CanSquash())
            {
                EnableSquashIndicator();
                canSquish = true;
                curTarget = other.gameObject;
                //Debug.Log("Can Squash");
            }
            else
            {
                DisableSquashIndicator();
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        iSquashable enemy = other.GetComponent<BasicEnemy>();
        if (enemy != null)
        {
            if (enemy.CanSquash())
            {
                EnableSquashIndicator();
                canSquish = true;
                curTarget = other.gameObject;
                // Debug.Log("Can Squash");
            }
            else
            {
                DisableSquashIndicator();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        DisableSquashIndicator();
        canSquish = false;
    }

    void EnableSquashIndicator()
    {
        squashIndicator.enabled = true;
    }

    void DisableSquashIndicator()
    {
        squashIndicator.enabled = false;
    }
}
