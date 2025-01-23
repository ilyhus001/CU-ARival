using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;
    public Transform startingPoint;
    private Transform endPoint;
    public LineRenderer lineRenderer;
    float elapsed;
    public NavMeshPath path;

    [SerializeField] private ARTrackedImageManager trackedImageManager;


    private void OnEnable() => trackedImageManager.trackedImagesChanged += OnChanged;

    private void OnDisable() => trackedImageManager.trackedImagesChanged -= OnChanged;

    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs){
                Debug.Log("Image");

        foreach (var newImage in eventArgs.added){
            string imageName = newImage.referenceImage.name;
            Transform targetObject = GameObject.Find(imageName).transform;
            if(targetObject != null){
                startingPoint.position = targetObject.transform.position;
                startingPoint.rotation = targetObject.transform.rotation;
                Debug.Log($"Starting point repositioned to {imageName} location");

            }
            else{
                Debug.Log($"Starting point repositioned to {imageName} location");
            }
        }
        foreach (var updatedImage in eventArgs.updated){
            string imageName = updatedImage.referenceImage.name;
            Transform targetObject = GameObject.Find(imageName).transform;
            if(targetObject != null){
                startingPoint.position = targetObject.transform.position;
                startingPoint.rotation = targetObject.transform.rotation;
                Debug.Log($"Starting point repositioned to {imageName} location");

            }
            else{
                Debug.Log($"Starting point repositioned to {imageName} location");
            }
        }
    }

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
}