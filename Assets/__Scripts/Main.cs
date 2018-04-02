using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum WeaponType
{
    none,
    simple,
    blaster,
    destroyer,
    shield,
    speedUp,
}

public class Main : MonoBehaviour
{
    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
   
    [Header("Set in Inspector")]
    public GameObject enemy_0;
    public GameObject enemy_1;
    public GameObject enemy_2;
    public GameObject prefabPowerUp;
    public WeaponType[] powerUpFrequency = new WeaponType[] {
    WeaponType.shield, WeaponType.speedUp };
    public const int NUM_LEVELS = 100;
    public WeaponDefinition[] weaponDefinitions;

 
    [Header("Set Dynamically")]
    private int numEnemies;
    private Vector3 spawnPoint;
    public float camWidth;
    public float camHeight;
    private float spawnTime = 1f;
    public Text scoreText;
    public Text highScoreText;
    public Text levelText;
    public Text nextLevelText;
    public Text errorText;

    private int score;
    private Level[] levels;
    static public int HIGH_SCORE = 0;
    static public Level CURRENT_LEVEL;
    private int mod = 2;

    void Awake() {
        S = this;
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();

        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }

        // Check for a high score in PlayerPrefs
        if (PlayerPrefs.HasKey("ShooterHighScore"))
        {
            HIGH_SCORE = PlayerPrefs.GetInt("ShooterHighScore");
            highScoreText.text = "High Score: " + HIGH_SCORE;

        }
    }

    void Start() {
        nextLevelText.enabled = false;
        errorText.enabled = false;
        GenerateLevels();
        CURRENT_LEVEL = levels[1];
        UpdateLevel();
        score = 0;
        UpdateScore();
    }

    void Update() { 
        if (score >= CURRENT_LEVEL.getMaxScore())
        {
            if (CURRENT_LEVEL.getMaxScore() >= NUM_LEVELS) {
                // if all levels are beat, print victory and restart game
                nextLevelText.text = "VICTORY!";
                nextLevelText.enabled = true;
                DelayedRestart(5f);
            }
            CURRENT_LEVEL = levels[CURRENT_LEVEL.getLevelNum()+1];
            UpdateLevel();
        }
    }

    public void GenerateLevels() {
        levels = new Level[NUM_LEVELS+1];
        for(int i=1; i<=NUM_LEVELS; i++) {
            levels[i] = new Level(i, 4.0f / i, 20 * i * i);
        }
    }

    public void DisplayError(string error) {
        errorText.text = error;
        errorText.enabled = true;
        Invoke("DisableErrorText", 2f);
    }

    private void DisableErrorText() {
        errorText.enabled = false;
    }

    public void UpdateLevel() {
        levelText.text = "Level: " + CURRENT_LEVEL.getLevelNum();
        spawnTime = CURRENT_LEVEL.getSpawnTime();
        nextLevelText.text = "Level " + CURRENT_LEVEL.getLevelNum();
        nextLevelText.enabled = true;
        CancelInvoke();
        // start spawning 3rd enemy type once past level 3
        if (CURRENT_LEVEL.getLevelNum() == 3)
        {
            mod = 3;
            DisplayError("New enemy unlocked!");
        }
        InvokeRepeating("Spawn", 0, spawnTime);
        Invoke("DisableNextLevelText", 2f);
        Debug.Log("New Level (" + CURRENT_LEVEL.getLevelNum() + ")\nSpawn Time: " + CURRENT_LEVEL.getSpawnTime() + "     Max Score: " + CURRENT_LEVEL.getMaxScore());
    }
    
    void DisableNextLevelText() {
        nextLevelText.enabled = false;
    }

    void Spawn() {
        GameObject enemy;
        spawnPoint = new Vector3((float)Random.Range(-camWidth+2, camWidth-2), camHeight+2, (float)0);
        numEnemies++;
        switch (numEnemies % mod)
        {
            case 1:
                enemy = (GameObject)Instantiate(enemy_0, spawnPoint, Quaternion.identity);
                break;
            case 2:
                enemy = (GameObject)Instantiate(enemy_2, spawnPoint, Quaternion.identity);
                break;
            default:
                enemy = (GameObject)Instantiate(enemy_1, spawnPoint, Quaternion.identity);
                break;
        }
        enemy.name = "Enemy " + numEnemies;
    }

    public void ShipDestroyed(Enemy e)
    {
        // Potentially generate a PowerUp
        if (Random.value <= e.powerUpDropChance)
        {
            // Random.value generates a value between 0 & 1 (though never == 1)
            // If the e.powerUpDropChance is 0.50f, a PowerUp will be generated
            // 50% of the time. For testing, it's now set to 1f.
            // Choose which PowerUp to pick
            // Pick one from the possibilities in powerUpFrequency
            int ndx = Random.Range(0, powerUpFrequency.Length);
            WeaponType puType = powerUpFrequency[ndx];
            // Spawn a PowerUp
            GameObject go = Instantiate(prefabPowerUp) as GameObject;
            PowerUp pu = go.GetComponent<PowerUp>();
            // Set it to the proper WeaponType
            pu.SetType(puType);
            // Set it to the position of the destroyed ship
            pu.transform.position = e.transform.position;
        }
    }

    public void DelayedRestart(float delay) {
        Invoke("Restart", delay);
    }

    public void Restart() {
        SceneManager.LoadScene("Main");
    }

    static public WeaponDefinition GetWeaponDefinition (WeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }
        return (new WeaponDefinition());
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        // Check for a high score in PlayerPrefs
        if (HIGH_SCORE <= score)
        {
            HIGH_SCORE = score;
            PlayerPrefs.SetInt("ShooterHighScore", HIGH_SCORE);
            highScoreText.text = "High Score: " + HIGH_SCORE;
        }
    }
}
 