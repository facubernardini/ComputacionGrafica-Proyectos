using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeLabel;
    public int totalPlayTimeInSeconds;

    private int hours, minutes;

    public GUIManager guiManager;

    // Start is called before the first frame update
    void Start()
    {
        hours = 0;
        minutes = 0;
        float updateTime = totalPlayTimeInSeconds / 360.0f;
        InvokeRepeating("UpdateTime", 0, updateTime);
    }

    private void UpdateTime()
    {
        minutes++;
        if (minutes > 59)
        {
            minutes = 0;
            hours++;
        }
        string h = "";
        string m = "";
        if (hours < 10) h = "0";
        h += hours;
        if (minutes < 10) m = "0";
        m += minutes;
        timeLabel.text = h + ":" + m + " AM";

        if (hours == 6)
        {
            guiManager.YouWon();
            CancelInvoke();
        }
    }
}
