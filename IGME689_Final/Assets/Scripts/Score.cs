using UnityEngine;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;

public class Score : MonoBehaviour
{
    [SerializeField] GameObject actualPointPrefab;
    public double spawnLongitude;
    public double spawnLatitude;

    public double pointDropLonitude;
    public double pointDropLatitude;
    public void CalculateScore(double longitude, double latitude)
    {
        spawnLongitude = FindFirstObjectByType<GameManager>().longitude;
        spawnLatitude = FindFirstObjectByType<GameManager>().latitude;

        GameObject actualPoint = Instantiate(actualPointPrefab, this.transform.parent);
        ArcGISLocationComponent locationComponent = actualPoint.AddComponent<ArcGISLocationComponent>();
        locationComponent.Position = new ArcGISPoint(spawnLongitude, spawnLatitude, 0, ArcGISSpatialReference.WGS84());

        pointDropLonitude = longitude;
        pointDropLatitude = latitude;
    }
}
