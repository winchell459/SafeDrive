using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Questions", menuName = "ScriptableObjects/Questions")]
public class Questions : ScriptableObject
{
    [TextArea]                  //[(insert something here)] is a modifier that affects how Unity handles something
    public string MyQuestion;
    [TextArea]
    public string Description;
    [TextArea]
    public string[] Answers;
    public int Answer;
}
