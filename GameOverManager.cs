using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    bool flag = true;
    public float restartDelay = 12f;
    public bool gameOv = false; 


    Animator anim;                          // Reference to the animator component.
    float restartTimer;                     // Timer to count up to restarting the level


    void Awake()
    {
        // Set up the reference.
        anim = GetComponent<Animator>();
        
    }

    
    void Update()
    {
        
        if (GameObject.Find("HealthBarPack"))
        {
            HealthBar myHealthbar = GameObject.Find("HealthBarPack").GetComponent<HealthBar>();
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController myPlayer = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();


            if (myHealthbar.hitpoint <= 0)
            {
                // tell the animator the game is over.
                anim.SetTrigger("GameOver");
                gameOv = true;
                if (flag)
                {
                    SoundManagerScript.StopMusic();
                    SoundManagerScript.PlaySound("gameOver");
                    flag = false;
                }

                // increment a timer to count up to restarting.
                restartTimer += Time.deltaTime;

                // if it reaches the restart delay
                if (restartTimer >= restartDelay)
                {
                    //  then load the menu
                    SceneManager.LoadScene("menu");
                }
            }
        }
    }
}