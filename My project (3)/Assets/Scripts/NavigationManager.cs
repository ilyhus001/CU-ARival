using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils; // For XR Origin

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;
    public Transform startingPoint; // Reference to AR Camera
    private Transform endPoint;
    public LineRenderer lineRenderer;
    float elapsed;
    public NavMeshPath path;

    [SerializeField] private ARTrackedImageManager trackedImageManager;
    private XROrigin xrOrigin; // Reference to XR Origin

    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
        instance = this;

        // Get reference to XR Origin
        xrOrigin = FindObjectOfType<XROrigin>();
        if (xrOrigin == null)
        {
            Debug.LogError("XR Origin not found!");
        }

        GameObject roomObject = GameObject.Find(findRoomScript.GetDestination());
        if (roomObject != null)
        {
            endPoint = roomObject.transform;
        }
    }

    private void OnEnable() => trackedImageManager.trackedImagesChanged += OnChanged;

    private void OnDisable() => trackedImageManager.trackedImagesChanged -= OnChanged;

    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log("Image detected");

        foreach (var newImage in eventArgs.added)
        {
            string imageName = newImage.referenceImage.name;
            GameObject targetObject = GameObject.Find(imageName);

            if (targetObject != null)
            {
                // Calculate the position difference between the detected image and the target object
                Vector3 offsetPosition = targetObject.transform.position - newImage.transform.position;
                Quaternion offsetRotation = targetObject.transform.rotation * Quaternion.Inverse(newImage.transform.rotation);

                // Manually update the XR Origin's position and rotation
                xrOrigin.transform.position += offsetPosition;
                xrOrigin.transform.rotation = offsetRotation;

                Debug.Log($"XR Origin manually adjusted to align with {imageName} location.");
            }
            else
            {
                Debug.LogError($"Could not find object named: {imageName}");
            }
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
