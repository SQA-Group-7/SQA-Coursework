using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float _startTime = -1;
    private float _endTime = -1;
    private bool _isTiming = false;

    public void StartTimer()
    {
        _isTiming = true;
        _startTime = Time.time;
    }

    public float GetTime()
    {
        if (_startTime != -1 && _endTime != -1)
        {
            return _endTime - _startTime;
        }
        else if (_startTime != -1)
        {
            return Time.time - _startTime;
        }
        else
        {
            return 0;
        }
    }

    public void PauseTimer()
    {
        _isTiming = false;
        _endTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_startTime != -1)
        {
            float timeDifference = GetTime();
            string minutes = ((int)timeDifference / 60).ToString();
            string seconds = (timeDifference % 60).ToString("00");
            string milliSeconds = (timeDifference % 60 % 1 * 1000).ToString("000");

            GetComponent<TextMeshProUGUI>().text = minutes + ":" + seconds + ":" + milliSeconds;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "0:00:000";
        }
    }
}
