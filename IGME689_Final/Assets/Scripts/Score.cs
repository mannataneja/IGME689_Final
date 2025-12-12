using UnityEngine;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using TMPro;


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
    public void CalculateScore(double longitude, double latitude)
    {
        spawnLongitude = FindFirstObjectByType<GameManager>().longitude;
        spawnLatitude = FindFirstObjectByType<GameManager>().latitude;

        GameObject actualPoint = Instantiate(actualPointPrefab, this.transform.parent);
        ArcGISLocationComponent locationComponent = actualPoint.AddComponent<ArcGISLocationComponent>();
        locationComponent.Position = new ArcGISPoint(spawnLongitude, spawnLatitude, 0, ArcGISSpatialReference.WGS84());

        pointDropLonitude = longitude;
        pointDropLatitude = latitude;

        difference = Mathf.Abs(((float)spawnLongitude - (float)pointDropLonitude) + ((float)spawnLatitude - (float)pointDropLatitude));
        actualScore = (int)(((55 - difference) / 55) * 10);
        scoreText.text = "Score : " + actualScore.ToString();
    }
}
