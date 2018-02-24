using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy {

    public void Move() {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void Update() {
        Move();
    }

}
