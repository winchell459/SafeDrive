using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHandler : MonoBehaviour
{
    public MasterControl MC;
    public GameObject StartPromptPanel, PretestPromptPanel, ScorePanel;
    public QuestionHandler UnitQuestion;
    public TestEvent Pretest, PracticalTest;
    
    

    // Update is called once per frame
    void Update()
    {
        if (MC.CurrentStage == MasterControl.UnitStages.Start)
        {
            if (!StartPromptPanel.activeSelf) StartPromptPanel.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                MC.NextStage();
                StartPromptPanel.SetActive(false);
            }
        }else if (MC.CurrentStage == MasterControl.UnitStages.PretestPrompt)
        {
            if (!PretestPromptPanel.activeSelf) PretestPromptPanel.SetActive(true);
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.Pretest)
        {
            if (PretestPromptPanel.activeSelf) PretestPromptPanel.SetActive(false);
            if (Pretest.EventCompleted)
            {
                MC.NextStage();
            }
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.PretestPause)
        {
            if (!ScorePanel.activeSelf)
            {
                ScorePanel.SetActive(true);
                TestEvent.ScoreCard card = Pretest.score;

                ScorePanel.GetComponent<ScorePanelHandler>().DisplayScore(card, true, card.Score != 0);
            }
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.Question)
        {
            if (ScorePanel.activeSelf) ScorePanel.SetActive(false);
            if (UnitQuestion.QuestionUI.activeSelf == false) UnitQuestion.DisplayQuestion();
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.QuestionScore)
        {

        }
        else if (MC.CurrentStage == MasterControl.UnitStages.PracticalTest)
        {

        }
        else if (MC.CurrentStage == MasterControl.UnitStages.Score)
        {

        }


    }

    public void ContinueButton()
    {
        MC.NextStage();
    }

    public void RetryButton()
    {
        MC.ReloadCurrentStage();
    }
}
