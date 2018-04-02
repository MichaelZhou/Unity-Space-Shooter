using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy {

    int leftOrRight;
    int angle;

    public override void Move() {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        if (leftOrRight == 1)
            tempPos.x -= speed * 2 * Time.deltaTime;
        else
            tempPos.x += speed * 2 * Time.deltaTime;
        pos = tempPos;
    }

    public void Start()
    {
        InvokeRepeating("GenerateDirection", 0f, 0.25f);
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

    public void GenerateDirection()
    {
        leftOrRight = Random.Range(0, 2);
    }

}
