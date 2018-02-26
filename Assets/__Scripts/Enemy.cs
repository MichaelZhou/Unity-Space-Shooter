using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Set in Inspector: Enemy")]
    public float speed = 5f;
    public float fireRate = 0.3f;
    public float health = 10f;
    public int score = 100;

    [Header("Set Dynamically")]
    public float camWidth;
    public float camHeight;

    void Awake() {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    public Vector3 pos {
        get {
            return (this.transform.position);
        }
        set {
            this.transform.position = value;
        }
    }

    void Update() {
        Move();
        // destroy objects if they exit bounds
        if (transform.position.y < -camHeight)
            Destroy(gameObject);
        if (transform.position.x > camWidth)
            Destroy(gameObject);
        if (transform.position.x < -camWidth)
            Destroy(gameObject);
    }
 

    public virtual void Move() {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

}
