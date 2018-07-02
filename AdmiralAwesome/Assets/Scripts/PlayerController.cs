using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int health;
    public int maxHealth;
    public bool immortal;

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
        if (!immortal)
        {
            if (game == null)
            {
                game = GameController._sharedInstance;
            }
            health -= amount;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
            game.UpdateHealth((float)(health) / (float)(maxHealth));
        }
    }

    public void Die()
    {
        game.GameOver();
    }
}
