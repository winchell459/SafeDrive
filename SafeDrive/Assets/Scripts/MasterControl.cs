using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Control
{
    [CreateAssetMenu(fileName = "MasterControl", menuName = "ScriptableObjects/MasterControl")]
    public class MasterControl : ScriptableObject
    {
        public Units[] Units;

        private int unitIndex = 0;

        public enum UnitSequence
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
        public UnitSequence CurrentState = UnitSequence.Start;

        public bool Paused
        {
            get { return Units[unitIndex].GetPaused(CurrentState); }
            //set { }
        }

        public void StartUnits()
        {
            unitIndex = 0;
            CurrentState = 0;
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

        public void NextSequence()
        {
            if (CurrentState < UnitSequence.Score)
            {
                CurrentState += 1;
                if (Units[unitIndex].GetScene(CurrentState) != SceneManager.GetActiveScene().name)
                {
                    SceneManager.LoadScene(Units[unitIndex].GetScene(CurrentState));
                }
            }
            else
            {
                CurrentState = 0;
                StartNextUnit();
            }
        }
    }

    [System.Serializable]
    public class Units
    {
        public string Name = "Unit_??";
        public SequenceScene[] SequenceScenes;


        public string GetScene(MasterControl.UnitSequence targetSequence)
        {
            string scene = SequenceScenes[0].SceneName;
            foreach (SequenceScene sequence in SequenceScenes)
            {
                if (sequence.MySequence <= targetSequence) scene = sequence.SceneName;


            }

            return scene;
        }

        public bool GetPaused(MasterControl.UnitSequence sequence)
        {
            bool paused = false;
            foreach (SequenceScene sequenceScene in SequenceScenes)
            {
                if (sequenceScene.MySequence == sequence)
                    paused = sequenceScene.Pause;
            }
            return paused;
        }
    }

    [System.Serializable]
    public class SequenceScene
    {
        public string SceneName;
        public MasterControl.UnitSequence MySequence;
        public bool Pause;
    }
}

