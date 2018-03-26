using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum WeaponType
{
    none,
    simple,
    blaster
}

public class Main : MonoBehaviour
{
    static public Main S;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
   
    [Header("Set in Inspector")]
    public GameObject enemy_0;
    public GameObject enemy_1;
    public float spawnTime = 1f;
    public WeaponDefinition[] weaponDefinitions;

    [Header("Set Dynamically")]
    private int numEnemies;
    private Vector3 spawnPoint;
    public float camWidth;
    public float camHeight;

    public Text scoreText;
    public Text highScoreText;
    private int score;
    static public int HIGH_SCORE = 0;

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
        InvokeRepeating("Spawn", 0, spawnTime);
        score = 0;
        UpdateScore();
           }

    void Spawn() {
        GameObject enemy;
        spawnPoint = new Vector3((float)Random.Range(-camWidth+2, camWidth-2), camHeight+2, (float)0);
        numEnemies++;
        switch (numEnemies % 2)
        {
            case 1:
                enemy = (GameObject)Instantiate(enemy_0, spawnPoint, Quaternion.identity);
                break;
            default:
                enemy = (GameObject)Instantiate(enemy_1, spawnPoint, Quaternion.identity);
                break;
        }
        enemy.name = "Enemy " + numEnemies;
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
 