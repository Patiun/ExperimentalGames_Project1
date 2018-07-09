using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEnemy : AbstractEnemy, iSquashable {

    public int scoreValue;

    private ObjectPooler pooler;
    private GameController game;

    // Use this for initialization
    void Start () {
        base.Init();
        canDropPowerup = false;
        pooler = ObjectPooler._sharedInstance;
        game = GameController._sharedInstance;
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
    }

    public bool CanSquash()
    {
        SquishFinder sf = SquishFinder._sharedInstance;
        Debug.Log((sf.CheckPoint(p1) + "," + sf.CheckPoint(p2) + "," + sf.CheckPoint(p3) + "," + sf.CheckPoint(p4)));
        return (sf.CheckPoint(p1) && sf.CheckPoint(p2) && sf.CheckPoint(p3) && sf.CheckPoint(p4));
    }

    public void Squash()
    {
        if (game == null)
        {
            game = GameController._sharedInstance;
        }
        game.AddScore(scoreValue);
        pooler.SpawnFromPool("Explosion", transform.position + transform.forward, Quaternion.identity);
        AudioManager.instance.Play("Explosion");
        gameObject.SetActive(false);
    }
}
