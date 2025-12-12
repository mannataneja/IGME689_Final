using UnityEngine;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Score : MonoBehaviour
{
    [SerializeField] public int maxScore;
    [SerializeField] public int actualScore;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject actualPointPrefab;
    public double spawnLongitude;
    public double spawnLatitude;

    public double pointDropLonitude;
    public double pointDropLatitude;

    public float difference;
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            scoreText.text = "Total Score : " + FindFirstObjectByType<GameManager>().cumulativeScore + "/" + FindFirstObjectByType<GameManager>().maxCumulativeScore;
        }
    }
    public IEnumerator CalculateScore(double longitude, double latitude)
    {
        spawnLongitude = FindFirstObjectByType<GameManager>().longitude;
        spawnLatitude = FindFirstObjectByType<GameManager>().latitude;

        Debug.Log("long" + spawnLongitude);
        Debug.Log("lat" + spawnLatitude);

        pointDropLonitude = longitude;
        pointDropLatitude = latitude;

        difference = Mathf.Abs(((float)spawnLongitude - (float)pointDropLonitude) + ((float)spawnLatitude - (float)pointDropLatitude));
        actualScore = (int)(((55 - difference) / 55) * maxScore);
        if(actualScore < 0)
        {
            actualScore = 0;
        }

        yield return new WaitForSeconds(1);

        GameObject actualPoint = Instantiate(actualPointPrefab, this.transform.parent);
        ArcGISLocationComponent locationComponent = actualPoint.AddComponent<ArcGISLocationComponent>();
        locationComponent.Position = new ArcGISPoint(spawnLongitude, spawnLatitude, 0, ArcGISSpatialReference.WGS84());

        scoreText.text = "Score : " + actualScore.ToString();
    }
}
