using UnityEngine;
using Esri.ArcGISMapsSDK.Components;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public int minutes;
    [SerializeField] public int seconds;
    public bool timeUp = false;

    [SerializeField] double longitude;
    [SerializeField] double latitude;

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
        if (Input.GetKey(KeyCode.M) && !timeUp)
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                minutes = FindFirstObjectByType<Timer>().minutes;
                seconds = FindFirstObjectByType<Timer>().seconds;
                LoadGlobeScene();
            }
        }
        if (Input.GetKey(KeyCode.S) && !timeUp)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                minutes = FindFirstObjectByType<Timer>().minutes;
                seconds = FindFirstObjectByType<Timer>().seconds;
                LoadStreetView();
            }
        }
        if (timeUp && SceneManager.GetActiveScene().buildIndex == 0)
        {
            minutes = FindFirstObjectByType<Timer>().minutes;
            seconds = FindFirstObjectByType<Timer>().seconds;
            LoadGlobeScene();
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
    public void LoadStreetView()
    {
        SceneManager.LoadScene(0);
    }
}
