using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingScreen : MonoBehaviour
{
    public float currTime;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI yarnText;
    // Start is called before the first frame update
    void Start()
    {
        currTime = Time.time;

        float timeSpent = currTime - GameManager.instance.getStartTime();
        float totalYarn = GameManager.instance.getCollectedYarnCount();

        // need to change the time into HH:MM:SS
        TimeSpan timeFormated = TimeSpan.FromSeconds(timeSpent);
        timeText.text = timeFormated.ToString().Substring(0, 8);
        yarnText.text = totalYarn.ToString() + " / 13";
    }

}
