using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSheetHandler : MonoBehaviour
{
    public GameObject CheatSheet;

    public void ToggleCheatSheet()
    {
        CheatSheet.SetActive(!CheatSheet.activeSelf);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) ToggleCheatSheet();
    }
}
