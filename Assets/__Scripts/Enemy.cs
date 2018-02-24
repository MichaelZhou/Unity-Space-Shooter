    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    [Header("Set in Inspector: Enemy")]
    public float speed = 5f;
    public float fireRate = 0.3f;
    public float health = 10f;
    public int score = 100;

    public Vector3 pos {
        get {
            return (this.transform.position);
        }
        set {
            this.transform.position = value;
        }
    }

}
