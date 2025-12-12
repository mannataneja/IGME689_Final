using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text minutesText;
    [SerializeField] TMP_Text secondsText;
    [SerializeField] TMP_Text timeUpText;

    [SerializeField] TMP_Text locationText;
    public int minutes;
    public int seconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeUpText.enabled = false;

        minutes = FindFirstObjectByType<GameManager>().minutes;
        seconds = FindFirstObjectByType<GameManager>().seconds;
        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");

        locationText.text = "Location: " + (FindFirstObjectByType<GameManager>().currentLocationIndex + 1).ToString() + "/" + FindFirstObjectByType<GameManager>().locations.Count;
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        while (minutes > 0 || seconds > 0)
        {
            if (seconds > 0)
            {
                seconds--;
            }
            else
            {
                seconds = 59;
                minutes--;
            }
            minutesText.text = minutes.ToString("00");
            secondsText.text = seconds.ToString("00");
            yield return new WaitForSeconds(1);
        }
        if (minutes == 0 && seconds == 0)
        {
            minutesText.text = minutes.ToString("00");
            secondsText.text = seconds.ToString("00");
            timeUpText.enabled = true;
            yield return new WaitForSeconds(3);
            FindFirstObjectByType<GameManager>().timeUp = true;
        }
    }
}
