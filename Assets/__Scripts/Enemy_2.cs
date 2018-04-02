using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy {
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    int leftOrRight;
    int angle;
    int timer = 0;

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

    void FireProjectile()
    {
        GameObject proj = Instantiate<GameObject>(projectilePrefab);
        proj.transform.position = transform.position;
        Rigidbody rigidB = proj.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.down * projectileSpeed;
    }

    public override void Update()
    {
        base.Update();
        timer = timer + 1;
        //shoot a projectile every 50 frames
        if (timer % 50 == 0)
        {
            FireProjectile();
        }
    }

}
