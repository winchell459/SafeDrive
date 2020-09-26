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

    public void DisplayQuestion(Question question, QuestionHandler handler)
    {
        QH = handler;
        QuestionText.text = question.MyQuestion;
        DescriptionText.text = question.Description;
        for (int i = 0; i < AnswerTexts.Length; i += 1)
        {
            AnswerTexts[i].text = question.Answers[i];
        }

        QuestionPanel.SetActive(true);
    }

    public void HideQuestion()
    {
        QuestionPanel.SetActive(false);
    }

    public void InputAnswer(int answer)
    {
        QH.InputAnswer(answer);
    }
}
