using UnityEngine;

public class SpawnAtMousePosition : MonoBehaviour
{
    public Camera cam;
    public GameObject dropPointPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !FindAnyObjectByType<GameManager>().pointDropped)
        {
            Debug.Log("CLICK");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("SPAWN");
                GameObject point = Instantiate(dropPointPrefab, hit.point, Quaternion.identity, this.transform.parent);
                FindAnyObjectByType<GameManager>().pointDropped = true;
            }
        }
    }
}
