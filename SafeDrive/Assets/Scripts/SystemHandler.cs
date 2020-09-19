using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemHandler : MonoBehaviour
{
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
    }

    private void handleMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.fullScreen = !Screen.fullScreen;

            if (!Screen.fullScreen)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }
    }
}
