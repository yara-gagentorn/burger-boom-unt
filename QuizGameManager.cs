using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizGameManager : MonoBehaviour {

    public QuizQuestions[] questions;
    private static List<QuizQuestions> unanswerd;
    private QuizQuestions currentQuestion;
    public static float quizScore = 0;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private float timeBetweenQuestions = 1;

    [SerializeField]
    private Text trueAnswerText;

    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private int scoreToWin=3;

    void Start() {
        if (unanswerd == null || unanswerd.Count == 0) {
            unanswerd = questions.ToList<QuizQuestions>();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        SetCurrentQuestion();
     }

    private void Update()
    {
        scoreText.text = "Your scores: " + quizScore +"/"+ scoreToWin;
        if (quizScore >= scoreToWin) {
         SoundManagerScript.PlaySound("gameWin");
         //   Debug.Log("WINNNNNNNN!!!!!!!!!!!!!");
            SceneManager.LoadScene("menu");
        }
    }

    void SetCurrentQuestion() {
        int randomQuestionIndex = Random.Range(0, unanswerd.Count);
        currentQuestion = unanswerd[randomQuestionIndex];
        factText.text = currentQuestion.question;

        if (!currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT!";
            falseAnswerText.text = "WRONG!";
        }
        else {
            trueAnswerText.text = "WRONG!";
            falseAnswerText.text = "CORRECT!";
        }

        
    }

    IEnumerator TransitionToNextQuestion() {
        unanswerd.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweenQuestions);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    public void UserSelectTrue() {
        animator.SetTrigger("True");
        if (currentQuestion.isTrue)
        {
            quizScore ++;
            scoreText.text = "Your scores: " + quizScore;         
        }
        else {
            //Debug.Log("Wrong");
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");
        if (!currentQuestion.isTrue)
        {
            //Debug.Log("Correct");
            quizScore ++;
            scoreText.text = "Your scores: " + quizScore;
            //Debug.Log(quizScore);
        }
        else
        {
            //Debug.Log("Wrong");
        }
        StartCoroutine(TransitionToNextQuestion());
    }
}
