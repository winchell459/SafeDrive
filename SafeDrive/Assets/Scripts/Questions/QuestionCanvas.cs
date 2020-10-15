using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{
    public GameObject QuestionPanel;
    public Text QuestionText, DescriptionText;
    public Text[] AnswerTexts;
    private QuestionHandler QH;
    public bool QuestionsActive;
    public GameObject IncorrectMessage;


    public void DisplayQuestion(Question question, QuestionHandler handler)
    {
        QH = handler;

        QuestionsActive = true;
        IncorrectMessage.SetActive(false);

        QuestionText.text = question.MyQuestion;
        DescriptionText.text = question.Description;
        for (int i = 0; i < AnswerTexts.Length; i += 1)
        {
            AnswerTexts[i].text = question.Answers[i];
        }
        
        
     

        QuestionPanel.SetActive(true);
        FindObjectOfType<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    

    public void HideQuestion()
    {
        QuestionsActive = false;
        QuestionPanel.SetActive(false);
    }

    public void InputAnswer(int answer)
    {
        QH.InputAnswer(answer);
    }
    public void DisplayIncorrectMessage()
    {
        IncorrectMessage.SetActive(true);
    }
}
