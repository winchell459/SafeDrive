using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanelHandler : MonoBehaviour
{
    public Text Title, Labels, Values;
    public Button ContinueButton, RetryButton;

    public void DisplayScore(TestEvent.ScoreCard scoreCard, bool isPretest, bool pass)
    {
        RetryButton.gameObject.SetActive(true);
        ContinueButton.gameObject.SetActive(true);
        if (isPretest)
        {
            RetryButton.gameObject.SetActive(false);
        }
        else
        {
            Title.text = "Test Results";
        }

        Labels.text = scoreCard.Labels;
        Values.text = scoreCard.Values;

        Labels.text += "\n Total";
        Values.text += "\n" + scoreCard.Score + "/" + scoreCard.Total;

        if(!isPretest && !pass)
        {
            ContinueButton.gameObject.SetActive(false);
        }
    }
}
