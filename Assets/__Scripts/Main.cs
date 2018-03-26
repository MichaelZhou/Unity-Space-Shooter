using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Awake() {
        S = this;
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();

        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }

    void Start() {
        InvokeRepeating("Spawn", 0, spawnTime);
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
}
 