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
    public Text waveText;
    public Image waveIndicator;

    public bool waveActive;
    public int waveNumber;
    public List<EnemySpawner> spawners;
    public float timeBetweenWaves;
    public float lengthOfWaves;
    public float spawnRate;
    public float spawnRateRamping;

    private int scoreAmount;
    private float healthPercentage;
    private float nextWaveTime;
    private float waveEndTime;
    private float timeElapsed;
    private List<GameObject> waveEnemies;

	// Use this for initialization
	void Start () {
        _sharedInstance = this;
        waveEnemies = new List<GameObject>();
        waveActive = false;
        nextWaveTime = Time.time + timeBetweenWaves;
        waveText.enabled = false;
        waveIndicator.fillAmount = 0f;
    }
	
	// Update is called once per frame
	void Update () {
		if (waveActive)
        {
            if (Time.time > waveEndTime)
            {
                StopSpawners();
                if (CheckWaveIsClearead())
                {
                    EndWave();
                }
            }
        } else
        {
            if (Time.time > nextWaveTime)
            {
                StartWave();
            } else
            {
                timeElapsed += Time.deltaTime;
                waveIndicator.fillAmount = (timeElapsed / timeBetweenWaves);
            }
        }
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

    private bool CheckWaveIsClearead()
    {
        foreach(GameObject enemy in waveEnemies)
        {
            if (enemy.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    public void AddToWave(GameObject enemy)
    {
        if (!waveEnemies.Contains(enemy))
        {
            waveEnemies.Add(enemy);
        }
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

    private void StopSpawners()
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.EndWave();
        }
    }

    public void EndWave()
    {
        waveActive = false;
        nextWaveTime = Time.time + timeBetweenWaves;
        StopSpawners();
        timeElapsed = 0;
        waveText.enabled = false;
    }

    public void StartWave()
    {
        waveActive = true;
        waveNumber++;
        waveEndTime = Time.time + lengthOfWaves;
        waveEnemies = new List<GameObject>();
        spawnRate += spawnRateRamping;
        foreach(EnemySpawner spawner in spawners)
        {
            spawner.StartWave();
            spawner.spawnRate = spawnRate;
        }
        waveIndicator.fillAmount = 1;
        waveText.text = "" + waveNumber;
        waveText.enabled = true;
    }
}
