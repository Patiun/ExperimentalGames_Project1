using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int health;
    public int maxHealth;
    public bool immortal;
    public float regenRate;
    public float regenLockout;
    public bool regening;
    public bool boosted;
    public float boostDuration;
    public float originalSpeed;
    public float boostSpeed;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;

    private float nextRegenTime;
    private float regenStartTime;
    private float boostOff;
    private GameController game;

	// Use this for initialization
	void Start () {
        health = maxHealth;
        game = GameController._sharedInstance;
        originalSpeed = fpc.m_WalkSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (!regening)
        {
            if (Time.time >= regenStartTime)
            {
                regening = true;
            }
        } else
        {
            if (Time.time >= nextRegenTime)
            {
                AddHP(1);
                nextRegenTime = Time.time + 1f / regenRate;
            }
        }
        if (boosted)
        {
            if (Time.time > boostOff)
            {
                boosted = false;
                fpc.m_WalkSpeed = originalSpeed;
            }
        }
    }

    public void AddHP(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (game == null)
        {
            game = GameController._sharedInstance;
        }
        game.UpdateHealth((float)(health) / (float)(maxHealth));
    }

    public void Damage(int amount)
    {
        if (!immortal)
        {
            regening = false;
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
            regenStartTime = Time.time + regenLockout;
        }
    }

    public void Die()
    {
        game.GameOver();
    }

    public void SpeedBoost()
    {
        Debug.Log("!!!!!!");
        boosted = true;
        fpc.m_WalkSpeed = boostSpeed;
        boostOff = Time.time + boostDuration;
    }
}
