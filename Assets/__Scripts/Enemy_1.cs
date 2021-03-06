﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy {

    int leftOrRight;
    int angle;

    public override void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        if (leftOrRight == 1)
            tempPos.x -= speed * Time.deltaTime;
        else
            tempPos.x += speed * Time.deltaTime;
        pos = tempPos;
    }

    void Start()
    {
        leftOrRight = Random.Range(0, 2);
        GameObject mainObject = GameObject.FindWithTag("MainCamera");
        if (mainObject != null)
        {
            main = mainObject.GetComponent<Main>();
        }
        if (mainObject == null)
        {
            Debug.Log("Cannot find 'MainCamera' script");
        }
    }

}