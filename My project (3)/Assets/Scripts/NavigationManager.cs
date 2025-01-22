using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;
    public Transform startingPoint;
    private Transform endPoint;
    public LineRenderer lineRenderer;
    float elapsed;
    public NavMeshPath path;

    public TMP_Text meters;

    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
        instance = this;

        GameObject roomObject = GameObject.Find(findRoomScript.GetDestination());
        if (roomObject != null)
        {
            endPoint = roomObject.transform;
        }
        meters.text = $"{CalculatePathDistance(path):F1} m";
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f)
        {
            elapsed -= 1.0f;
            if (endPoint != null)
            {
                NavMesh.CalculatePath(startingPoint.position, endPoint.position, NavMesh.AllAreas, path);
                lineRenderer.positionCount = path.corners.Length;
                lineRenderer.SetPositions(path.corners);

                float distance = CalculatePathDistance(path);
                meters.text = $"{distance:F1} m"; 
            }
        }
    }

    public void UpdateNavigationTarget(string roomName)
    {
        
        GameObject roomObject = GameObject.Find(roomName);

        if (roomObject != null)
        {
            endPoint = roomObject.transform;  
            Debug.Log("Navigation room updated: " + roomName);
        }
        else
        {
            Debug.LogError("Room not found: " + roomName);
        }
    }

    private float CalculatePathDistance(NavMeshPath navPath)
    {
        if (navPath.corners.Length < 2)
        {
            return 0f;
        }

        float totalDistance = 0f;

        for (int i = 0; i < navPath.corners.Length - 1; i++)
        {
            totalDistance += Vector3.Distance(navPath.corners[i], navPath.corners[i + 1]);
        }

        return totalDistance;
    }
}
