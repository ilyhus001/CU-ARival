using System.Collections.Generic;

using UnityEngine;

using UnityEngine.AI;

public class NavigationManager : MonoBehaviour
{

    public Transform visitorHead;

    private List<Transform> TargetLocations;
    
    public Transform targetLocationParent;

    private Transform selectedTargetLocation;
    private Transform startPoint;

    public LineRenderer lineRenderer;

    float elapsed; 
    public NavMeshPath path;
    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;

        this.TargetLocations = new List<Transform>();
        foreach(Transform location in this.targetLocationParent){
            if(location != this.targetLocationParent){
                this.TargetLocations.Add(location);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Transform endPoint = chosenEndPoint();
        elapsed += Time.deltaTime;
        if(elapsed > 1.0f){
            elapsed -= 1.0f;
            NavMesh.CalculatePath(startPoint.position, endPoint.position,NavMesh.AllAreas, path);
        }

        lineRenderer.positionCount=path.corners.Length;
        lineRenderer.SetPositions(path.corners);
    }

    private Transform chosenEndPoint()
    {
        return TargetLocations[0];
    }

    public void NavigateTo(GameObject go){}
}
