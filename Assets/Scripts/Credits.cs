using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public Transform CreditText;
    public float DelayStart = 0.5f;
    public float ScrollSpeed = 1;
    public int ExitClickCount = 2;
    float creditsStart;

    // Start is called before the first frame update
    void Start()
    {
        creditsStart = Time.time;
        ScrollSpeed *= Screen.height / 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        if (creditsStart + DelayStart < Time.time)
        {
            CreditText.position += new Vector3(0, ScrollSpeed * Time.deltaTime, 0);
        }

        if(Input.GetMouseButtonDown(0))
        {
            ExitClickCount -= 1;
            if(ExitClickCount == 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }
}
