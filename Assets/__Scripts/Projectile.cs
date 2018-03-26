using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Renderer rend;

    [Header("Set Dynamically")]
    public float camWidth;
    public float camHeight;

    public Rigidbody rigid;

    [SerializeField]
    private WeaponType _type;

    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }

    void Awake() {
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }

    public void SetType(WeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
    }
}
