using UnityEngine;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Vector2> locations;
    public int currentLocationIndex = 0;

    [SerializeField] public int minutes;
    [SerializeField] public int seconds;
    public bool timeUp = false;

    [SerializeField] public double longitude;
    [SerializeField] public double latitude;

    public bool pointDropped = false;

    public int cumulativeScore = 0;
    public int maxCumulativeScore = 0;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            longitude = FindFirstObjectByType<FirstPersonController>().gameObject.GetComponent<ArcGISLocationComponent>().Position.X;
            latitude = FindFirstObjectByType<FirstPersonController>().gameObject.GetComponent<ArcGISLocationComponent>().Position.Y;
        }
            
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.M) && !timeUp) || timeUp)
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                minutes = FindFirstObjectByType<Timer>().minutes;
                seconds = FindFirstObjectByType<Timer>().seconds;
                LoadGlobeScene();
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && !timeUp)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                minutes = FindFirstObjectByType<Timer>().minutes;
                seconds = FindFirstObjectByType<Timer>().seconds;
                StartCoroutine(LoadStreetView());
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if(currentLocationIndex < 2)
                {
                    cumulativeScore += FindFirstObjectByType<Score>().actualScore;
                    maxCumulativeScore += FindFirstObjectByType<Score>().maxScore;

                    pointDropped = false;
                    minutes = 3;
                    seconds = 0;
                    timeUp = false;
                    StartCoroutine(NewLocation());
                }
                else
                {
                    cumulativeScore += FindFirstObjectByType<Score>().actualScore;
                    maxCumulativeScore += FindFirstObjectByType<Score>().maxScore;
                    LoadTotalScore();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                cumulativeScore += FindFirstObjectByType<Score>().actualScore;
                maxCumulativeScore += FindFirstObjectByType<Score>().maxScore;
                LoadTotalScore();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !timeUp)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                minutes = FindFirstObjectByType<Timer>().minutes;
                seconds = FindFirstObjectByType<Timer>().seconds;
                StartCoroutine(LoadStreetView());
            }
        }
    }
    void CreateSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadGlobeScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(1);
    }
    public IEnumerator LoadStreetView()
    {
        SceneManager.LoadScene(0);

        yield return new WaitForSeconds(1);

        FindFirstObjectByType<ArcGISMapComponent>().OriginPosition = new ArcGISPoint(locations[currentLocationIndex].x, locations[currentLocationIndex].y, 0, ArcGISSpatialReference.WGS84());
        FindFirstObjectByType<FirstPersonController>().GetComponent<ArcGISLocationComponent>().Position = new ArcGISPoint(locations[currentLocationIndex].x, locations[currentLocationIndex].y, 20, ArcGISSpatialReference.WGS84());
        FindFirstObjectByType<ArcGISCameraComponent>().GetComponent<ArcGISLocationComponent>().Position = new ArcGISPoint(locations[currentLocationIndex].x, locations[currentLocationIndex].y, 150, ArcGISSpatialReference.WGS84());

        longitude = locations[currentLocationIndex].x;
        latitude = locations[currentLocationIndex].y;

        FindFirstObjectByType<Weather>().longitude = longitude;
        FindFirstObjectByType<Weather>().latitude = latitude;
        FindFirstObjectByType<Weather>().CallWeatherAPI();

        FindFirstObjectByType<RiverQuery>().QueryRivers(longitude, latitude);
    }
    public IEnumerator NewLocation()
    {
        Debug.Log("new scene");
        SceneManager.LoadScene(0);
        currentLocationIndex++;

        yield return new WaitForSeconds(1);

        FindFirstObjectByType<ArcGISMapComponent>().OriginPosition = new ArcGISPoint(locations[currentLocationIndex].x, locations[currentLocationIndex].y, 0, ArcGISSpatialReference.WGS84());
        FindFirstObjectByType<FirstPersonController>().GetComponent<ArcGISLocationComponent>().Position = new ArcGISPoint(locations[currentLocationIndex].x, locations[currentLocationIndex].y, 20, ArcGISSpatialReference.WGS84());
        FindFirstObjectByType<ArcGISCameraComponent>().GetComponent<ArcGISLocationComponent>().Position = new ArcGISPoint(locations[currentLocationIndex].x, locations[currentLocationIndex].y, 150, ArcGISSpatialReference.WGS84());

        longitude = locations[currentLocationIndex].x;
        latitude = locations[currentLocationIndex].y;

        FindFirstObjectByType<Weather>().longitude = longitude;
        FindFirstObjectByType<Weather>().latitude = latitude;
        FindFirstObjectByType<Weather>().CallWeatherAPI();

        FindFirstObjectByType<RiverQuery>().QueryRivers(longitude, latitude);
    }
    public void LoadTotalScore()
    {
        SceneManager.LoadScene(2);
    }
}
