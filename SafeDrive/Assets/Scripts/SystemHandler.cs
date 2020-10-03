using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SystemHandler : MonoBehaviour
{
    public Control.MasterControl MC;
    private void Awake()
    {
        if(FindObjectOfType<SystemHandler>() && FindObjectOfType<SystemHandler>() != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        handleMenu();
        handleMainMenu();
    }

    private void handleMainMenu()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MC.StartUnits();
            }
        }
    }

    private void handleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.fullScreen = !Screen.fullScreen;

            if(!Screen.fullScreen)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
            
        }
    }
}
