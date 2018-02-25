using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public GameObject enemy_0;
    public GameObject enemy_1;
    public int numEnemies = 15;
    private Vector3 spawnPoint;

    void Start()
    {
        for (int i = 1; i <= numEnemies; i++)
        {
            GameObject enemy;
            spawnPoint = new Vector3((float)Random.Range(-25, 25), Random.Range(20, 40), (float)0);
            switch (i % 2)
            {
                case 1:
                    enemy = (GameObject)Instantiate(enemy_0, spawnPoint, Quaternion.identity);
                    break;
                default:
                    enemy = (GameObject)Instantiate(enemy_1, spawnPoint, Quaternion.identity);
                    break;
            }
            enemy.name = "Enemy " + i;
        }
    }
}
 