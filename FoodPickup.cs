using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class FoodPickup : MonoBehaviour {
    public string soundOfEating = "eatFruit";
    public float healthAffect = 0;
    public float speedAffect = 0;
    public float speedAffectDuration = 5;
    public string whereToTeleport;
    public float score = 0;
    public bool alcohol = false;
    public float drunkDuration = 10;
    public bool onion = false;
    public float blurDuration = 5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate( 0,90 * Time.deltaTime, 0); //rotation
	}

    IEnumerator ChangeSpeed () { // changing speed of player
        // find the player
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController myPlayer = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        //save default speed
        float speedWalkBefore = myPlayer.m_WalkSpeed;
        float speedRunBefore = myPlayer.m_RunSpeed;
        // change the speed 
        myPlayer.m_WalkSpeed += speedAffect;
        myPlayer.m_RunSpeed += speedAffect;
        //  setup duration of effect
        yield return new WaitForSeconds(speedAffectDuration);
        Debug.Log("Wait for it");
        // change speed back
        myPlayer.m_WalkSpeed = speedWalkBefore;
        myPlayer.m_RunSpeed = speedRunBefore;
    }

    // health affect
    private void ChangeHealth() {
        HealthBar myHealthbar = GameObject.Find("HealthBarPack").GetComponent<HealthBar>();
        myHealthbar.AffectHealth(healthAffect);
    }

    // teleport the player
    public void ChangePosition() {
        //get the position of the object from string and send player there
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController myPlayer = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        GameObject destination = GameObject.Find(whereToTeleport);
        if (destination != null) {
            // get the position of the object
            Vector3 destinationCoord = destination.transform.position;
            // setup new players coordinates
            myPlayer.transform.position = destinationCoord;
        }
    }

    // add scores
    private void ChangeScore() {
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController myPlayer = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        myPlayer.score += score;
        //Debug.Log("Change scores");
    }

    // setup temporary blur vision 
    IEnumerator ChangeVision() {
        BlurOptimized blur = GameObject.Find("FirstPersonCharacter").GetComponent<BlurOptimized>();
        blur.enabled = true;
        yield return new WaitForSeconds(blurDuration);
        blur.enabled = false;
    }

    // setup temporary drunk effect
   IEnumerator GetDrunk() {
        MotionBlur drunk = GameObject.Find("FirstPersonCharacter").GetComponent<MotionBlur>();
        drunk.enabled = true;
        yield return new WaitForSeconds(drunkDuration);
        drunk.enabled = false;
    }

    // show the message about effects
    IEnumerator  ShowUserMessage() {
        // find message place and show the message
        Text myMessage = GameObject.Find("Msg").GetComponent<Text>();
        string fullMessage = "";

        // create the message (depends on effects)
        if (alcohol) { fullMessage += "YOU GOT DRUNK!  "; }
        if (onion) { fullMessage += "BLUR VISION!  "; }
        if (healthAffect > 0) { fullMessage += "health+" + healthAffect + "  "; }
        if (healthAffect < 0) { fullMessage += "health" + healthAffect + "  "; }
        if (speedAffect > 0) { fullMessage += "speed+" + speedAffect + "  "; }
        if (speedAffect < 0) { fullMessage += "speed" + speedAffect + "  "; }
        myMessage.text = fullMessage;
        yield return new WaitForSeconds(4);// show message for 4 seconds
        myMessage.text = "";// clear the message
    }

    // apply effects on collision
    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "FPSController")
        {
            SoundManagerScript.PlaySound(soundOfEating);
            ChangeScore();
            StartCoroutine(ChangeSpeed());
            ChangeHealth();
            ChangePosition();
            
            if (onion) {StartCoroutine(ChangeVision()); }
            if (alcohol) { StartCoroutine(GetDrunk()); }
            StartCoroutine(ShowUserMessage());
            // Debug.Log("I am here!");
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
