using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Import the SceneManager for scene loading
using TMPro;

public class findRoomScript : MonoBehaviour
{
    public Button myButton; // Drag your button into this field in the Inspector
    public Button helpButton;
    public TMP_Dropdown dropDown;

    

    void Start()
    {
        if (dropDown == null)
        {
        Debug.LogError("Dropdown is not assigned!");
        return;
        }

        Debug.Log("Dropdown initial state: " + dropDown.gameObject.activeSelf);
        if (myButton == null)
        {
            Debug.LogError("myButton is not assigned in the Inspector!");
            return;
        }
        if (helpButton == null)
        {
            Debug.LogError("helpButton is not assigned in the Inspector!");
            return;
        }

        myButton.onClick.AddListener(LoadNavMeshScene);
    }

    public string getRoom()
    {
        // Get the index of the selected item in the dropdown
        int selectedIndex = dropDown.value;
        
        // Return the text of the selected option
        return dropDown.options[selectedIndex].text;
    }

    public void LoadNavMeshScene()
    {
        Debug.Log("Button clicked in room!"+ getRoom());
        print("HI" + getRoom());

        // Load NavScene
        SceneManager.LoadScene("NavScene");
    }
}