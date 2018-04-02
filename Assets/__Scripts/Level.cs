using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    private int levelNum;
    private float spawnTime;
    private int maxScore;

    public Level() { }

    public Level(int num, float time, int score)
    {
        levelNum = num;
        spawnTime = time;
        maxScore = score;
    }

    public int getLevelNum() {
        return levelNum;
    }

    public float getSpawnTime() {
        return spawnTime;
    }

    public int getMaxScore() {
        return maxScore;
    }
}
