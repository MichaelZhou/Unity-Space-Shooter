using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject enemy_0;
    public GameObject enemy_1;
    public float spawnTime = 1f;

    [Header("Set Dynamically")]
    private int numEnemies;
    private Vector3 spawnPoint;
    public float camWidth;
    public float camHeight;

    void Awake() {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
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
}
 