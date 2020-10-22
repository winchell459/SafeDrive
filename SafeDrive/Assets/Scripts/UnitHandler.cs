using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHandler : MonoBehaviour  //UnitHandler accesses the MasterControl program and functions (cross between utility class and script for game object)       
{
    public MasterControl MC;
    public GameObject StartPromptPanel, PretestPromptPanel, ScorePanel;
    public QuestionHandler UnitQuestion;
    public TestEvent Pretest, PracticalTest;

    public Marker PretestMarkers, PracticalMarkers;

    // Update is called once per frame
    void Update()
    {
        if(MC.CurrentStage == MasterControl.UnitStages.Start)
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
            if (!PretestMarkers.MarkerActive) PretestMarkers.MarkerActive = true;
            if(Pretest.EventCompleted)
            {
                MC.NextStage();
            }
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.PretestPause)
        {
            if(!ScorePanel.activeSelf)
            {
                ScorePanel.SetActive(true);
                TestEvent.ScoreCard card = Pretest.score;

                ScorePanel.GetComponent<ScorePanelHandler>().DisplayScore(card, true, card.Score != 0); //displays score & results if score does not equal 0
            }
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.Question)
        {
            if (ScorePanel.activeSelf) ScorePanel.SetActive(false);
            if (!UnitQuestion.QuestionActive) UnitQuestion.DisplayQuestion();      
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.QuestionScore)
        {
            if (UnitQuestion.QuestionActive) UnitQuestion.HideQuestion();
            MC.NextStage();
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.PracticalTest)
        {
            if (!PracticalMarkers.MarkerActive) PracticalMarkers.MarkerActive = true;
            if (PracticalTest.EventCompleted) MC.NextStage();
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.Score)
        {
            if (!ScorePanel.activeSelf)
            {
                ScorePanel.SetActive(true);
                TestEvent.ScoreCard card = PracticalTest.score;
                bool passed = card.Score / card.Total > 0.7f;
                ScorePanel.GetComponent<ScorePanelHandler>().DisplayScore(card, true, passed);
            }
        }


    }

    public void ContinueButton()
    {
        MC.NextStage();
        Debug.Log("Continue Button Pressed");
    }

    public void RetryButton()
    {
        MC.ReloadCurrentStage();
    }
}
