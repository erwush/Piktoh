using UnityEngine;
using TMPro;
using System;

public class Cycle : MonoBehaviour
{
    public float duration;
    public float time;
    public float day;
    public int hour;
    public int min;
    public TextMeshProUGUI timeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        duration = 1440f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > duration)
        {
            time = 0;
            day++;
            hour = 0;
            min = 0;
        }
        float hourTime = (time / duration) * 24f;
        hour = (int)hourTime;
        min = (int)((hourTime - hour) * 60f);
        timeText.text = "Day: " + day.ToString() + "<br>" + hour.ToString("D2") + ":" + min.ToString("D2");

    }
}



public enum Waktu
{
    Pagi,
    Siang,
    Sore,
    Malam
}
