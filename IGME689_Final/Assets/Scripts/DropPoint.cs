using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;

public class DropPoint : MonoBehaviour
{
    public ArcGISLocationComponent locationComponent;
    private void Start()
    {
        locationComponent = gameObject.AddComponent<ArcGISLocationComponent>();

        Score();
    }
    public void Score()
    {
        StartCoroutine(FindFirstObjectByType<Score>().CalculateScore(locationComponent.Position.X, locationComponent.Position.Y));
    }

}
