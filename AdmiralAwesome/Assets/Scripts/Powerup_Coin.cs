using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Coin : AbstractPowerUp, ICollectable, IPooledObject {

    public int scoreValue;

    private GameController game;
	// Use this for initialization
	void Start () {
        game = GameController._sharedInstance;
	}
	
	// Update is called once per frame
	new void Update ()
    {
        base.Update();
    }

    public void Pickup()
    {
        if (game == null) { game = GameController._sharedInstance; }
        game.AddScore(scoreValue);
        gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        if (game == null) { game = GameController._sharedInstance; }
    }
}
