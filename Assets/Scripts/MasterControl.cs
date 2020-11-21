using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MasterControl", menuName = "ScriptableObjects/MasterControl")]
public class MasterControl : ScriptableObject
{
    public Units[] Units;
    public string Credits = "Credits";

    private int unitIndex = 0;
    public enum UnitStages
    {
        Start,
        PretestPrompt,
        Pretest,
        PretestPause,
        Question,
        QuestionScore,
        PracticalTest,
        Score,
        Debug
    }
    public UnitStages CurrentStage = UnitStages.Start;

    public float PassingScore { get { return Units[unitIndex].PassingScore; } }
    public bool Paused
    {
        get { return Units[unitIndex].GetPaused(CurrentStage); }
        // set {; }
    }

    public bool SceneLoading;
    public void StartUnits()  //resets units back to Start
    {
        unitIndex = 0;
        CurrentStage = 0;
        SceneManager.LoadScene(Units[0].GetScene(0));
    }

    public void StartNextUnit()
    {
        unitIndex += 1;
        if(unitIndex < Units.Length)
        {
            SceneLoading = true;
            SceneManager.LoadScene(Units[unitIndex].GetScene(0));
        }
        else
        {
            SceneManager.LoadScene(Credits);
        }
    }

    public void NextStage()
    {
        if(CurrentStage < UnitStages.Score)
        {
            CurrentStage += 1;
            if(Units[unitIndex].GetScene(CurrentStage) != SceneManager.GetActiveScene().name)
            {
                SceneManager.LoadScene(Units[unitIndex].GetScene(CurrentStage));
            }
        }
        else if (CurrentStage != UnitStages.Debug)
        {
            CurrentStage = 0;
            StartNextUnit();
        }
    }
    public void ReloadCurrentStage()
    {
        CurrentStage -= 1;
        SceneManager.LoadScene(Units[unitIndex].GetScene(CurrentStage));
    }
}

//[System.Serializable]
//public class Units
//{
//    public string Name = "Units_??"; //changes default name Element ?? to Name
//    public StageScene[] StageScenes;
//    public float PassingScore = 0.7f;

//    public string GetScene(MasterControl.UnitStages targetSequence)
//    {
//        string scene = StageScenes[0].SceneName;
//        foreach(StageScene sequence in StageScenes)
//        {
//            if (sequence.MyStage <= targetSequence) scene = sequence.SceneName;
//        }

//        return scene;
//    }
//    public bool GetPaused(MasterControl.UnitStages stage)
//    {
//        bool paused = true;
//        if (stage == MasterControl.UnitStages.Debug) return false;
//        foreach(StageScene stageScene in StageScenes)
//        {
//            if (stageScene.MyStage == stage)
//                paused = stageScene.Pause;
//        }
//        return paused;
//    }
//}

//[System.Serializable]
//public class StageScene
//{
//    public string SceneName;
//    public MasterControl.UnitStages MyStage;
//    public bool Pause;
//}