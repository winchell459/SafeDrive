using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "ScriptableObjects/Unit")]
public class Units : ScriptableObject
{
    public string Name = "Units_??"; //changes default name Element ?? to Name
    public StageScene[] StageScenes;
    public float PassingScore = 0.7f;

    public string GetScene(MasterControl.UnitStages targetSequence)
    {
        string scene = StageScenes[0].SceneName;
        foreach (StageScene sequence in StageScenes)
        {
            if (sequence.MyStage <= targetSequence) scene = sequence.SceneName;
        }

        return scene;
    }
    public bool GetPaused(MasterControl.UnitStages stage)
    {
        bool paused = true;
        if (stage == MasterControl.UnitStages.Debug) return false;
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
