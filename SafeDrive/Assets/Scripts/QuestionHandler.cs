using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public GameObject QuestionUI;
    //public Text QuestionText, DescriptionText, AnswerText;
    public Question CurrentQuestion;
    public Question[] Questions;

    // Start is called before the first frame update
    void Start()
    {
        DisplayQuestion();
    }

    public void DisplayQuestion()
    {
        QuestionUI.GetComponent<QuestionCanvas>().DisplayQuestion(CurrentQuestion, this);
    }

    public void InputAnswer(int answer)
    {
        Debug.Log(answer == CurrentQuestion.Answer ? "Correct" : "Incorrect");

        //string response == answer == CurrentQuestion.Answer ? "Correct" : "Incorrect");

    }
}
