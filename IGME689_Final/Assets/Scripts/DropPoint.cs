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

        StartCoroutine(Score());
    }
    public IEnumerator Score()
    {
        yield return new WaitForSeconds(1);

        FindFirstObjectByType<Score>().CalculateScore(locationComponent.Position.X, locationComponent.Position.Y);
    }

}
