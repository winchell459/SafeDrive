using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public GameObject QuestionUI;
    public Text QuestionText, DescriptionText, AnswerText;
    public Question CurrentQuestion;

    // Start is called before the first frame update
    void Start()
    {
        DisplayQuestion();
    }

    public void DisplayQuestion()
    {
        QuestionText.text = CurrentQuestion.MyQuestion;
        DescriptionText.text = CurrentQuestion.Description;
        string answers = "";
        foreach(string answer in CurrentQuestion.Answers)
        {
            answers += answer + "\n";
        }
        AnswerText.text = answers;
        QuestionUI.SetActive(true);
    }
    
}
