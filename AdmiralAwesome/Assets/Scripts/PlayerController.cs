using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        MoveX(Input.GetAxis("Horizontal"));
        MoveZ(Input.GetAxis("Vertical"));

    }

    public void MoveX(float amnt)
    {
        rb.velocity = transform.right * amnt * moveSpeed;
    }

    public void MoveZ(float amnt)
    {
        rb.velocity = transform.forward * amnt * moveSpeed;
    }
}
