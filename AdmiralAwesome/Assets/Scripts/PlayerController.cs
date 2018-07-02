using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int health;
    public int maxHealth;

    private GameController game;

	// Use this for initialization
	void Start () {
        health = maxHealth;
        game = GameController._sharedInstance;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {

    }
}
