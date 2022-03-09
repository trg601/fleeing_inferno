using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerLabel : MonoBehaviour
{
    private Text label;

    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<Text>();

        //Update label from playerPrefs
        float timer = PlayerPrefs.GetFloat("timer", 0);
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.RoundToInt(timer % 60);
        string minuteStr = minutes == 1 ? minutes.ToString() + " minute" : minutes.ToString() + " minutes";
        string secondStr = seconds == 1 ? seconds.ToString() + " seconds" : seconds.ToString() + " secondss";
        label.text = string.Format("In {0} and {1}", minuteStr, secondStr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
