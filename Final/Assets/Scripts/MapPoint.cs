using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;

public class MapPoint : MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<ArcGISLocationComponent>();
    }

}