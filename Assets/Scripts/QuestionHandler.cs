using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public GameObject QuestionUI;
    //public Text QuestionText, DescriptionText, AnswerText;
    private Question CurrentQuestion;
    public Question[] Questions;
    public bool[] QuestionResults;
    private int index;
    public bool QuestionActive { get { return QuestionUI.GetComponent<QuestionCanvas>().QuestionsActive; } }

    // Start is called before the first frame update
    void Start()
    {
        //DisplayQuestion();
        CurrentQuestion = Questions[0];
        QuestionResults = new bool[Questions.Length];
    }

    public void DisplayQuestion() //displays question number from QuestionCanvas
    {
        QuestionUI.GetComponent<QuestionCanvas>().DisplayQuestion(CurrentQuestion, this);
    }

    public void HideQuestion()
    {
        QuestionUI.GetComponent<QuestionCanvas>().HideQuestion();
    }

    public void InputAnswer(int answer)
    {
        //Debug.Log(answer == CurrentQuestion.Answer ? "Correct" : "Incorrect");

        //string response == answer == CurrentQuestion.Answer ? "Correct" : "Incorrect");

        QuestionResults[index] = answer == CurrentQuestion.Answer;
        if(answer == CurrentQuestion.Answer)
        {
            index += 1;
            if (index > Questions.Length - 1)
            {
                FindObjectOfType<UnitHandler>().MC.NextStage();
            }
            else
            {
                CurrentQuestion = Questions[index];
                DisplayQuestion();
            }
        }
        else
        {
            //display "incorrect answer"
            QuestionUI.GetComponent<QuestionCanvas>().DisplayIncorrectMessage();
        }
    }
}
