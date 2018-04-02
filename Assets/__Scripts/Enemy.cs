using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Set in Inspector: Enemy")]
    public float speed = 5f;
    public float fireRate = 0.3f;
    public float health = 10f;
    public int scoreValue;

    [Header("Set Dynamically")]
    public float camWidth;
    public float camHeight;


    public Main main;

    void Start()
    {
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

    public virtual void Update() {
        Move();
    }
 

    public virtual void Move() {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter (Collision coll)
    {
        GameObject otherGo = coll.gameObject;
        if (otherGo.tag == "ProjectileHero")
        {
            Projectile p = otherGo.GetComponent<Projectile>();
            health -= Main.GetWeaponDefinition(p.type).damageOnHit;
            if (health <= 0)
            {
                Destroy(this.gameObject);
                main.AddScore(scoreValue);
            }
            Destroy(otherGo); //destroys the projectile
        }
       else  if (otherGo.tag == "ProjectileEnemy")
        {
            Destroy(otherGo); //destroys the projectile
        }
        else if (otherGo.tag != "ProjectileEnemy")
        {
            print("Enemy hit by non-ProjectileHero: " + otherGo.name);
        }
    }

}
