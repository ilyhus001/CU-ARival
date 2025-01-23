using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;
using Unity.VisualScripting;
using TMPro;
using System.Collections;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;
    public Transform startingPoint; 
    private Transform endPoint;
    public LineRenderer lineRenderer;
    float elapsed;
    public NavMeshPath path;
    [SerializeField] private Animator animator;

    [SerializeField] private ARTrackedImageManager trackedImageManager;
    private XROrigin xrOrigin; 
    public Button button; // Assign your button in the inspector
    public TextMeshProUGUI textMeshPro; // Assign your TextMeshPro component in the inspector
    // distance management

    private bool isUpdating = false;
    private Text distanceText;
    private WaitForSeconds waitTime;

    void Awake()
    {
        // Ensure singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        path = new NavMeshPath();
    }

    void Start()
    {
        animator.SetBool("ButtonPress",true);

        xrOrigin = FindObjectOfType<XROrigin>();
        if (xrOrigin == null)
        {
            Debug.LogError("XR Origin not found!");
            return;
        }

        startingPoint = xrOrigin.Camera.transform;

        string targetName = Scanner.imageName;
        if (!string.IsNullOrEmpty(targetName))
        {
            GameObject targetObject = GameObject.Find(targetName);
            if (targetObject != null)
            {
                xrOrigin.transform.position = targetObject.transform.position;
                xrOrigin.transform.rotation = targetObject.transform.rotation;

                Debug.Log($"XR Origin aligned to {targetName} at position: {targetObject.transform.position}");
            }
            else
            {
                Debug.LogError($"Target object {targetName} not found in the 3D map!");
            }
        }
        else
        {
            Debug.LogWarning("Target name from Scanner script is empty!");
        }
        //distance check
        distanceText = GetComponent<Text>();
        waitTime = new WaitForSeconds(0.5f); 
        button.onClick.AddListener(StartUpdatingDistance);
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 0.5f) 
        {
            elapsed = 0f;
            if (endPoint != null && startingPoint != null)
            {
                Vector3 currentPosition = startingPoint.position;
                
                NavMesh.CalculatePath(currentPosition, endPoint.position, NavMesh.AllAreas, path);
                
                
                if (path.corners.Length > 0)
                {
                    lineRenderer.positionCount = path.corners.Length;
                    lineRenderer.SetPositions(path.corners);
                }
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
    //DISTANCE CHECK

    void StartUpdatingDistance()
    {
        textMeshPro.text = "PLEASE";
        if (!isUpdating)
        {
            isUpdating = true;
            StartCoroutine(UpdateDistance());
        }
    }
   IEnumerator UpdateDistance(){

        while (true) 
        {
            textMeshPro.text = "TEST";
            if (endPoint != null)
            {
                textMeshPro.text = "CHEESE";
                NavMesh.CalculatePath(startingPoint.position, endPoint.position, NavMesh.AllAreas, path);

                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    float pathLength = 0;
                    for (int i = 0; i < path.corners.Length - 1; i++)
                    {
                        pathLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                    }

                    textMeshPro.text = "Distance: " + pathLength.ToString("F2") + " units";
                }
                else
                {
                    textMeshPro.text = "No path found.";
                }
            }
            else
            {
                textMeshPro.text = "No destination set.";
            }

            yield return waitTime; 
        }
    }
}