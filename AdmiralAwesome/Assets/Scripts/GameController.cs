using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour {

    public static GameController _sharedInstance;

    public Text score;
    public Image health;
    public Vector3 vignetteScale;
    public Color vignetteColor;
    public PostProcessVolume postProcessing;
    public GameObject HUD;
    public GameObject GameOverMenu;
    public Text gameOverScore;
    public FirstPersonController fpc;

    public int waveNumber;

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
        gameOverScore.text = "Carpal Tunnel Score: " + scoreAmount;
    }

    public void UpdateHealth(float amnt)
    {
        health.fillAmount = amnt;
        vignetteScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(255, 0, 0), 1 - amnt);
        vignetteColor = new Color(vignetteScale.x, vignetteScale.y, vignetteScale.z,1f);
        /*
        Vignette v;
        postProcessing.profile.TryGetSettings(out v);
        if (v!=null)
        {
            ColorParameter cp = v.color;
            cp.value = vignetteColor;
        }*/
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        HUD.SetActive(false);
        GameOverMenu.SetActive(true);
        fpc.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
