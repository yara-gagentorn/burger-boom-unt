using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
    public static AudioClip eatFruitSound;
    public static AudioClip getDrunkSound;
    public static AudioClip gameOverSound;
    public static AudioClip gameWinSound;
    public static AudioClip gameBackground;
    static AudioSource audioSrc;

	// Use this for initialization
	void Start () {
        
        eatFruitSound = Resources.Load<AudioClip>("eatFruit");
        getDrunkSound = Resources.Load<AudioClip>("getDrunk");
        gameOverSound = Resources.Load<AudioClip>("gameOver");
        gameWinSound = Resources.Load<AudioClip>("gameWin");
        gameBackground = Resources.Load<AudioClip>("background1");

        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlaySound(string clip)
    {
        switch (clip) {
            case "eatFruit":
                audioSrc.PlayOneShot(eatFruitSound);
                break;
            case "getDrunk":
                audioSrc.PlayOneShot(getDrunkSound);
                break;
            case "gameOver":
                audioSrc.PlayOneShot(gameOverSound);
                break;
            case "gameWin":
                audioSrc.PlayOneShot(gameWinSound);
                break;
            case "background1":
                audioSrc.PlayOneShot(gameBackground);
                break;

        }
    }
    public static void StopMusic()
    {
        Debug.Log("Stop!!!");
        audioSrc.Stop();
    }
   }
