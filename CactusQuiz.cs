using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// collision with Cactus
public class CactusQuiz : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController myPlayer = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

        //player has to have 10 scores for playing quiz
        if (other.name == "FPSController" && myPlayer.score >= 10) 
        {
            //if there is enough scores - start the quiz
            SceneManager.LoadScene("quiz");
        }
        else {
            // if there is not enough scores then show the message
            StartCoroutine(cactusMessage());
        }
    }

    IEnumerator cactusMessage() {
        Text myMessage = GameObject.Find("Msg").GetComponent<Text>();
        myMessage.color = Color.red; // change message color to red
        myMessage.text = "NOT ENOUGH FOOD SCORES FOR QUEST!";
        yield return new WaitForSeconds(3f);
        myMessage.text = "";// wait for 3 seconds and hide the message
        }
 }
