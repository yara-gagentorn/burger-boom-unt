using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinManager : MonoBehaviour {
    bool flag = true;
    public float Delay = 6;         
    [HideInInspector] public bool gameWin = false;
    public float scoreToWin = 10;
    Animator anim;  // amination for Win
    float delayTimer;  // delay
    

    void Start() {
        anim = GetComponent<Animator>();
    }

    
    void Update() {
        if (flag)
        { // make sure it is playing only one time
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController myPlayer = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
            HealthBar myHealthbar = GameObject.Find("HealthBarPack").GetComponent<HealthBar>();// get access to health points
            if (myPlayer.score >= scoreToWin && myHealthbar.hitpoint > 0)
            { // make shure health points more than 0 and there is enough scores
                anim.SetTrigger("GameWin"); // set trigger for playing animation

                GameObject myHealthbarImage = GameObject.Find("HealthBarPack");
                myHealthbarImage.SetActive(false);// hide the health bar

                SoundManagerScript.StopMusic();// stop background music
                SoundManagerScript.PlaySound("gameWin");

                gameWin = true;
                flag = false; // set the flag to false
            }
        }

        if (!flag)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= Delay) // wait a certain amount of time  
            {
                SceneManager.LoadScene("scene2");// load next level
            }
        }    
        
    }
}