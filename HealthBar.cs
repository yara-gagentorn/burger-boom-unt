using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Image currentHealthbar;
    [SerializeField] private Text FoodScore;

    public float hitpoint = 150;
    private float maxHitpoint = 150;

    private void Start() {
        UpdateHealthbar();
    }

    private void UpdateHealthbar() {
        float ratio = hitpoint / maxHitpoint;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController myPlayer = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        GameWinManager gameWinM = GameObject.Find("GameWin").GetComponent<GameWinManager>();
        FoodScore.text = "Food scores: " + myPlayer.score + "/" + gameWinM.scoreToWin;

    }

    public void AffectHealth(float healthAffect) {
        hitpoint += healthAffect;
        if (hitpoint < 0)
        {
            hitpoint = 0;
            Debug.Log("Dead!");
        }
        if (hitpoint > maxHitpoint)
        {
            hitpoint = maxHitpoint;
        }
        UpdateHealthbar();
    }
}
