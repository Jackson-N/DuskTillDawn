using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public int currentHour = 10;
    public int currentMinute = 00;
    string timeString = "10:00";
    public TextMeshPro textTimerBox;

    public int currentWorldTime;

    //public GameObject textBox = GetComponent<TextMeshPro>();

    void Start()
    {
        ParseTime(timeString);
        InvokeRepeating("UpdateTime", 0f, 1f); // Call UpdateTime every second

        textTimerBox = GetComponent<TextMeshPro>();
    }

    void UpdateTime()
    {
        // Print current time
        //Debug.Log($"Current time: {FormatTime()}");
        Debug.Log(System.DateTime.Now);
        ParseTime(timeString);

        // Increase minute
        currentMinute++;

        // Check if minute reached 60, then reset it to 0 and increase hour
        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;

            // Check for midnight and adjust accordingly
            if (currentHour >= 12)
            {
                currentHour = 1;
            }
        }

        // Stop the timer at 6:00
        if (currentHour == 6)
        {
            CancelInvoke("UpdateTime");
            Debug.Log("Game Over: Win");
        }

        if (currentMinute <= 9)
        {
            timeString = currentHour + ":0" + currentMinute;
            textTimerBox.text = timeString;
        }
        else
        {
            timeString = currentHour + ":" + currentMinute;
            textTimerBox.text = timeString;
        }

        
    }

    // Parse time string to int
    void ParseTime(string timeText)
    {
        string[] parts = timeText.Split(':');
        currentHour = int.Parse(parts[0]);
        currentMinute = int.Parse(parts[1]);

        //return currentHour;
    }
}
