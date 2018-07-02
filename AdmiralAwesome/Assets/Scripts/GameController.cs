using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController _sharedInstance;

    public Text score;
    public Image health;

    private int scoreAmount;
    private float healthPercentage;

	// Use this for initialization
	void Start () {
        _sharedInstance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(int amount)
    {
        scoreAmount += amount;
        score.text = "Score: " + scoreAmount;
    }

    public void UpdateHealth(float amnt)
    {
        health.fillAmount = amnt;
    }
}
