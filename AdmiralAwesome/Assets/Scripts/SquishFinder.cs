using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquishFinder : MonoBehaviour {

    public static SquishFinder _sharedInstance;

    public float detectionSize;
    public float w, h;
    public float tolerance;
    public Vector3 center;
    public SpriteRenderer marker;
    public float distFromCamera;
    public Image squashIndicator;
    public GameObject curTarget;
    public bool canSquish;
    public float squishRate;
    public LayerMask layerMask;
    public float radius;

    private float nextSquishTime;

    //ANIM
    public GameObject animObject;
    Animator handAnim;

    // Use this for initialization
    void Start () {
        _sharedInstance = this;
        nextSquishTime = Time.time;
        

        //Anim
        handAnim = animObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position,radius, transform.forward,out hit,Mathf.Infinity,layerMask.value))
        {
            GameObject other = hit.collider.gameObject;
            iSquashable enemy = other.GetComponent<iSquashable>();
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
                    canSquish = false;
                }
            }
            else
            {
                canSquish = false;
            }
        } else
        {
            canSquish = false;
        }
        if (Time.time >= nextSquishTime)
        {
            if (canSquish)
            {
                EnableSquashIndicator();
                if (Input.GetAxis("Fire1") != 0)
                {
                    Squash();
                    handAnim.SetTrigger("pinch"); //Anim
                }
            } else
            {
                DisableSquashIndicator();
            }
        }
	}

    public void Squash()
    {
        curTarget.GetComponent<iSquashable>().Squash();
        nextSquishTime = Time.time + 1f / squishRate;
    }

    public bool CheckPoint(Vector3 point)
    {
        center = new Vector3(Screen.width / 2f, Screen.height / 2f, distFromCamera);
        point = Camera.main.WorldToScreenPoint(point);
        w = Screen.width * detectionSize;
        h = w;
        bool xGood = (point.x <= center.x + w/2f && point.x >= center.x + (w*tolerance)/2f) || (point.x <= center.x - (w*tolerance) / 2f && point.x >= center.x - w / 2f);
        bool yGood = (point.y <= center.y + h / 2f && point.y >= center.y + (h * tolerance) / 2f) || (point.y <= center.y - (h * tolerance) / 2f && point.y >= center.y - h / 2f);
        return (xGood && yGood);
    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("!");
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward,out hit,Mathf.Infinity,layerMask.value))
        {
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
                    canSquish = false;
                }
            }
            else
            {
                canSquish = false;
            }
        }
        else
        {
            canSquish = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask.value))
        {
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
                    canSquish = false;
                }
            }
            else
            {
                canSquish = false;
            }
        }
        else
        {
            canSquish = false;
        }
    }*/

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
