using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
    static public Hero _hero; // singleton
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;


    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;

    // Weapon fields
    public Weapon[] weapons;

    private GameObject lastTriggerGo = null;

    void Awake() {
        if (_hero == null)
            _hero = this; // setting singleton

    }

    void Update () {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;

        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;

        transform.position = pos;
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
	}

    //when the hero bumps into an enemy 
    void OnTriggerEnter(Collider other) {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        if (go == lastTriggerGo)
            return;
        lastTriggerGo = go;
        if (go.tag == "Enemy" || go.tag == "ProjectileEnemy") {
            shieldLevel--;
            Destroy(go);
        }

        else if (go.tag == "PowerUp") {
            // If the shield was triggerd by a PowerUp
            AbsorbPowerUp(go);
            }
  
        else
            print("Triggered by non-enemy: " + go.name);  
    }
    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type)
        {
            case WeaponType.shield: // If it's the shield
                shieldLevel++;
                SoundManagerScript.PlaySound("shield");
                break;

            case WeaponType.speedUp: // If it's the speedup
                speed += 3;
                SoundManagerScript.PlaySound("speed");
                break;
        
            default:
                break;
        }
        pu.AbsorbedBy(this.gameObject);
    }


    public float shieldLevel {
        get {
            return (_shieldLevel);
        }
        set {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0) {
                Destroy(this.gameObject);
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }





}
