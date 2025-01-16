using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class findRoomScript : MonoBehaviour
{
    public Button myButton;
    public Button helpButton;
    public TMP_Dropdown dropDown;
    // public NavigationManager navManager; // Reference to the NavigationManager script
    private static string destination;


    void Start()
    {

        if (dropDown == null)
        {
            Debug.LogError("Dropdown is not assigned!");
            return;
        }

        if (myButton == null)
        {
            Debug.LogError("myButton is not assigned in the Inspector!");
            return;
        }

        myButton.onClick.AddListener(OnNavigateButtonClicked);
    }

    public string GetRoom()
    {
        int selectedIndex = dropDown.value;
        return dropDown.options[selectedIndex].text;
    }

    private void OnNavigateButtonClicked()
    {
        string selectedRoom = GetRoom();
        Debug.Log("Selected Room: " + selectedRoom);

        // Pass the selected room to the NavigationManager
        SetDestination(selectedRoom.Replace(" ", "").Trim());

        // Optionally load the navigation scene
        SceneManager.LoadScene("NavScene");
    }


     public void SetDestination(string roomName)
    {
        // Try to find the GameObject by its name

        destination = roomName;

    }

    public static string GetDestination(){
        return destination;
    }
}