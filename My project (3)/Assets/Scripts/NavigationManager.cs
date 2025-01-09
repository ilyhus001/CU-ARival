using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{

    private Transform startingPoint;

    private Transform endPoint;

    public LineRenderer lineRenderer;

    float elapsed; 
    public NavMeshPath path;

    /*New additions*/

    private List<Transform> TargetLocations;

    public Transform targetLocationParent;
    public RectTransform buttonsParent;
    public GameObject buttonsPrefab;
    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;

        this.TargetLocations = new List<Transform>();
        foreach (Transform c in this.targetLocationParent)
        {
            if (c != this.targetLocationParent) this.TargetLocations.Add(c);
        }
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

    private void OnVisitorCreated()
    {
        Debug.Log("VisitorCreated");

        foreach(var t in this.TargetLocations)
        {
            var b=GameObject.Instantiate(buttonsPrefab, buttonsParent);
            b.GetComponentInChildren<TextMeshProUGUI>().text = t.gameObject.name;
            b.GetComponent<Button>().onClick.AddListener(() => { this.NavigateTo(t); });
        }
    }

    public void NavigateTo(Transform target){
        this.endPoint = target;
    }
}