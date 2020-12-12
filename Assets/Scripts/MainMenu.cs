using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public MasterControl MC;
    public Button[] Buttons;
    public string Credits = "Credits";

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <= MC.unlockedUnit; i++)
        {
            Buttons[i].interactable = true;
        }
    }

    public void SelectUnitButton(int unitIndex)
    {
        MC.LoadUnit(unitIndex);
    }

    public void SelectCreditsButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Credits);
    }
}
