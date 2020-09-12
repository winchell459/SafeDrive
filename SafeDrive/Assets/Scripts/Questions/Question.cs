using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "ScriptableObjects/Question")]
public class Question : ScriptableObject
{
    [TextArea]
    public string MyQuestion;
    [TextArea]
    public string Description;
    [TextArea]
    public string[] Answers;
    public int Answer;
}
