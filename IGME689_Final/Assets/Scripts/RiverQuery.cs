using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class RiverQuery : MonoBehaviour
{
    private const int RadiusMeters = 20000;

    private void Start()
    {
        QueryRivers(-122.4194, 37.7749);
    }
    public void QueryRivers(double longitude, double latitude)
    {
        string url =
            "https://services.arcgis.com/P3ePLMYs2RVChkJx/ArcGIS/rest/services/USA_Rivers_and_Streams/FeatureServer/0/query" +
            $"?geometry={longitude},{latitude}" +
            "&geometryType=esriGeometryPoint" +
            "&inSR=4326" +
            "&spatialRel=esriSpatialRelIntersects" +
            $"&distance={RadiusMeters}" +
            "&units=esriSRUnit_Meter" +
            "&outFields=Name,Miles" +
            "&returnGeometry=false" +
            "&f=geojson";

        Debug.Log(url);
        StartCoroutine(QueryCoroutine(url));
    }

    private IEnumerator QueryCoroutine(string url)
    {
        using UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
            yield break;
        }

        ParseAndPrintRivers(request.downloadHandler.text);
    }

    private void ParseAndPrintRivers(string json)
    {
        JObject root = JObject.Parse(json);
        JToken[] features = root["features"]?.ToArray();

        List<string> rivers = new List<string>();

        if (features != null)
        {
            foreach (var feature in features)
            {
                string name = feature["properties"]?["Name"]?.ToString();
                double miles = feature["properties"]?["Miles"]?.ToObject<double>() ?? 0;

                if (!string.IsNullOrWhiteSpace(name))
                {
                    rivers.Add($"{name} ({miles:F2} miles)");
                }
            }
        }

        if (rivers.Count == 0)
        {
            Debug.Log("Nearby rivers are: None");
        }
        else
        {
            Debug.Log("Nearby rivers are: " + string.Join(", ", rivers.Distinct()));
        }
    }

}
