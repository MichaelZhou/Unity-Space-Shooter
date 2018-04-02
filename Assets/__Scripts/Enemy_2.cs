using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy {
    public GameObject projectilePrefab;
    public GameObject barrel;
    private Renderer barrelRend;
    public float projectileSpeed = 20f;
    int leftOrRight;
    int angle;
    int timer = 0;
    Vector3 offset = new Vector3 (0f, 2f, 0f);

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
        barrel = transform.Find("BarrelEnemy").gameObject;
        barrelRend = barrel.GetComponent<Renderer>();
    }

    public void GenerateDirection()
    {
        leftOrRight = Random.Range(0, 2);
    }

    void FireProjectile()
    {
        GameObject proj = Instantiate<GameObject>(projectilePrefab);
        proj.transform.position = barrel.transform.position -  offset;
        Rigidbody rigidB = proj.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.down * projectileSpeed;
    }

    public override void Update()
    {
        base.Update();
        timer = timer + 1;
        //shoot a projectile every 90 frames
        if (timer % 90 == 0)
        {
            FireProjectile();
        }
    }

}
