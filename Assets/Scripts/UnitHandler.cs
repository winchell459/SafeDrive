using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHandler : MonoBehaviour  //UnitHandler accesses the MasterControl program and functions (cross between utility class and script for game object)       
{
    public MasterControl MC;
    public GameObject StartPromptPanel, PretestPromptPanel, ScorePanel;
    public QuestionHandler UnitQuestion;
    public TestEvent Pretest, PracticalTest;
    public TestEvent PretestInitializer, PracticalTestInitializer;

    public Marker PretestMarkers, PracticalMarkers;

    private void Start()
    {
        MC.SceneLoading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(MC.CurrentStage == MasterControl.UnitStages.Start && !MC.SceneLoading)
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
            if (PretestMarkers && !PretestMarkers.MarkerActive) PretestMarkers.MarkerActive = true;
            if(Pretest && Pretest.EventCompleted)
            {
                MC.NextStage();
            } else if (PretestInitializer && !PretestInitializer.Initialized)
            {
                PretestInitializer.Initialized = true;
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
        else if (MC.CurrentStage == MasterControl.UnitStages.PracticalTest && !MC.SceneLoading)
        {
            if (PracticalMarkers && !PracticalMarkers.MarkerActive) PracticalMarkers.MarkerActive = true;
            if (PracticalTest && PracticalTest.EventCompleted) MC.NextStage();
            else if (PracticalTestInitializer && !PracticalTestInitializer.Initialized)
            {
                PracticalTestInitializer.Initialized = true;
            }
        }
        else if (MC.CurrentStage == MasterControl.UnitStages.Score)
        {
            if (!ScorePanel.activeSelf)
            {
                ScorePanel.SetActive(true);
                TestEvent.ScoreCard card = PracticalTest.score;
                float score = (float)card.Score / (float)card.Total;
                bool passed = score > MC.PassingScore;
                //Debug.Log("Score/Total: " + score + " > " + MC.PassingScore);
                ScorePanel.GetComponent<ScorePanelHandler>().DisplayScore(card, false, passed);
            }
        }

    }

    public void ContinueButton()
    {
        MC.NextStage();
        //Debug.Log("Continue Button Pressed");
    }

    public void RetryButton()
    {
        ScorePanel.SetActive(false);
        MC.ReloadCurrentScoringStage();
    }

    public void ResetCurrentStage()
    {
        MC.ReloadCurrentStage();
    }
}
