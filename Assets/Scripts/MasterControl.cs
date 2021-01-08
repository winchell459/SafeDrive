using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MasterControl", menuName = "ScriptableObjects/MasterControl")]
public class MasterControl : ScriptableObject
{
    public static MasterControl MC;

    public bool TouchControls;
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
        get { return unitIndex < Units.Length ? Units[unitIndex].GetPaused(CurrentStage) : false; }//return (if this is true) ? (then return this) : (else return this);
        // set {; }
    }

    private void OnEnable()
    {
        MC = this;
        Debug.Log("MC setup");
    }

    public bool SceneLoading;
    public void StartUnits()  //resets units back to Start
    {
        unitIndex = 0;
        CurrentStage = 0;
        SceneManager.LoadScene(Units[0].GetScene(0));
    }
    public int unlockedUnit = 0;

    public void UnlockMaxUnit()
    {
        //unlockedUnit = Mathf.Max(unlockedUnit, unitIndex);
        if (unlockedUnit < unitIndex)
        {
            unlockedUnit = unitIndex;
        }
    }
    public void StartNextUnit()
    {
        unitIndex += 1;
        if(unitIndex < Units.Length)
        {
            UnlockMaxUnit();
            LoadUnit(unitIndex);
        }
        else
        {
            SceneManager.LoadScene(Credits);
        }
    }

    public void LoadUnit(int unitIndex)
    {
        this.unitIndex = unitIndex;
        CurrentStage = 0;
        SceneLoading = true;
        SceneManager.LoadScene(Units[unitIndex].GetScene(0));
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
        SceneLoading = true;
        SceneManager.LoadScene(Units[unitIndex].GetScene(CurrentStage));
    }

    public void ReloadCurrentScoringStage()
    {
        CurrentStage -= 1;
        ReloadCurrentStage();
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