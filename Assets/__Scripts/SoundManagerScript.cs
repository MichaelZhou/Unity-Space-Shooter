using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    // Use this for initialization

    public static AudioClip playerBulletSound, enemy_0DeathSound, enemy_1DeathSound, enemy_2DeathSound, powerUpSpeedSound, powerUpShieldSound,
        enemyBulletSound, levelUpSound, powerUpFireSound;
    static AudioSource audioSrc;

	void Start () {
        playerBulletSound = Resources.Load<AudioClip>("laser1");
        enemy_0DeathSound = Resources.Load<AudioClip>("Explosion+1");
        enemy_1DeathSound = Resources.Load<AudioClip>("Explosion+3");
        enemy_2DeathSound = Resources.Load<AudioClip>("Explosion+5");
        powerUpSpeedSound = Resources.Load<AudioClip>("powerUpSpeed");
        powerUpShieldSound = Resources.Load<AudioClip>("powerUpShield");
        enemyBulletSound = Resources.Load<AudioClip>("laser7");
        levelUpSound = Resources.Load<AudioClip>("piano");
        powerUpFireSound = Resources.Load<AudioClip>("powerUpFire");

        audioSrc = GetComponent<AudioSource> ();
        

	}
	
	// Update is called once per frame
	void Update () {
       
	}

   public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "fire":
                audioSrc.PlayOneShot(playerBulletSound);
                break;
            case "0death":
                audioSrc.PlayOneShot(enemy_0DeathSound);
                break;
            case "1death":
                audioSrc.PlayOneShot(enemy_1DeathSound);
                break;
            case "2death":
                audioSrc.PlayOneShot(enemy_2DeathSound);
                break;
            case "speed":
                audioSrc.PlayOneShot(powerUpSpeedSound);
                break;
            case "shield":
                audioSrc.PlayOneShot(powerUpShieldSound);
                break;
            case "doubleTap":
                audioSrc.PlayOneShot(powerUpFireSound);
                break;
            case "enemyFire":
                audioSrc.PlayOneShot(enemyBulletSound);
                break;
            case "levelUp":
                audioSrc.PlayOneShot(levelUpSound);
                break;

        }

    }
}
