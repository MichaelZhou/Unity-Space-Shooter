using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngledEnemyMovement : Enemy {

    int leftOrRight;
    int angle;
    public override void Move()
    {
        Vector3 tempPos = pos;
        if (leftOrRight == 1)
        {
            tempPos.x -= speed * Time.deltaTime;
        }
        else if (leftOrRight == 2)
        {
            tempPos.x += speed * Time.deltaTime;
        }
        pos = tempPos;
    }

    void Start()
    {
        leftOrRight = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update () {
        Move();
	}
}
