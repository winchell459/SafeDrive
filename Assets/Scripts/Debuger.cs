using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debuger : MonoBehaviour
{
    public Text DebugText;
    public static Debuger Logger;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            DebugText.gameObject.SetActive(!DebugText.gameObject.activeSelf);
        }
    }

    private void Awake()
    {
        Logger = this;
        DebugText = GetComponent<Text>();
    }

    public static void MyLog(string message)
    {
        if (Logger)
        {
            Logger.DebugText.text = message + "\n" + Debuger.Logger.DebugText.text;
        }
    }
}
