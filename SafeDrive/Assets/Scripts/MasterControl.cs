using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "MasterControl", menuName = "ScriptableObjects/MasterControl")]
public class MasterControl : ScriptableObject
{
    public Units[] Units;

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
        Score
    }
    public UnitStages CurrentStage = UnitStages.Start;

    public float PassingScore { get { return Units[unitIndex].PassingScore; } }

    public bool Paused
    {
        get { return Units[unitIndex].GetPaused(CurrentStage); }
        //set { }
    }

    public void StartUnits()
    {
        unitIndex = 0;
        CurrentStage = 0;
        SceneManager.LoadScene(Units[0].GetScene(0));
    }

    public void StartNextUnit()
    {
        unitIndex += 1;

        if (unitIndex < Units.Length)
        {
            SceneManager.LoadScene(Units[unitIndex].GetScene(0));
        }
    }

    public void NextStage()
    {
        if (CurrentStage < UnitStages.Score)
        {
            CurrentStage += 1;
            if (Units[unitIndex].GetScene(CurrentStage) != SceneManager.GetActiveScene().name)
            {
                SceneManager.LoadScene(Units[unitIndex].GetScene(CurrentStage));
            }
        }
        else
        {
            CurrentStage = 0;
            StartNextUnit();
        }
    }

    public void ReloadCurrentStage()
    {
        SceneManager.LoadScene(Units[unitIndex].GetScene(CurrentStage));
    }
}


[System.Serializable]
public class Units
{
    public string Name = "Unit_??";
    public StageScene[] StageScenes;
    public float PassingScore = 0.7f;

    public string GetScene(MasterControl.UnitStages targetStage)
    {
        string scene = StageScenes[0].SceneName;
        foreach (StageScene stage in StageScenes)
        {
            if (stage.MyStage <= targetStage) scene = stage.SceneName;


        }

        return scene;
    }

    public bool GetPaused(MasterControl.UnitStages stage)
    {
        bool paused = true;
        foreach (StageScene stageScene in StageScenes)
        {
            if (stageScene.MyStage == stage)
                paused = stageScene.Pause;
        }
        return paused;
    }
}

[System.Serializable]
public class StageScene
{
    public string SceneName;
    public MasterControl.UnitStages MyStage;
    public bool Pause;
}


