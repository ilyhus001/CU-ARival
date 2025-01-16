using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;
    public Transform startingPoint;

    private Transform endPoint;

    public LineRenderer lineRenderer;

    float elapsed; 
    public NavMeshPath path;
    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
        instance = this;
        GameObject roomObject = GameObject.Find(findRoomScript.GetDestination());
        endPoint = roomObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed > 1.0f){
            elapsed -= 1.0f;
            NavMesh.CalculatePath(startingPoint.position, endPoint.position,NavMesh.AllAreas, path);
        }

        lineRenderer.positionCount=path.corners.Length;
        lineRenderer.SetPositions(path.corners);
    }


    public void NavigateTo(GameObject go){}


}