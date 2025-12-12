using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int minutes;
    [SerializeField] public int seconds;

    [SerializeField] TMP_Text minutesText;
    [SerializeField] TMP_Text secondsText;

    [SerializeField] ArcGISMapComponent map;
    [SerializeField] double longitude;
    [SerializeField] double latitude;

    private void Start()
    {
        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");

        StartCoroutine(Timer());


    }

    public IEnumerator Timer()
    {
        while(minutes > 0 || seconds > 0)
        {
            if(seconds > 0)
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
    }

}
