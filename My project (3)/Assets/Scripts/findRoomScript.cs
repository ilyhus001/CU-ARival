using UnityEngine;
using UnityEngine.UI;

public class findRoomScript : MonoBehaviour
{
    public Button myButton; // Drag your button into this field in the Inspector

    void Start()
    {
        if (myButton == null)
        {
            Debug.LogError("myButton is not assigned in the Inspector!");
            return;
        }

        myButton.onClick.AddListener(LoadNavMeshScene);
    }

    public void LoadNavMeshScene()
    {
        Debug.Log("Button clicked!");
        print("HI");
    }
}
