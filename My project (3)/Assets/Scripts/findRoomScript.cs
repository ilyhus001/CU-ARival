using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class findRoomScript : MonoBehaviour
{
    public Button myButton;
    public Button helpButton;
    public TMP_Dropdown dropDown;
    private NavigationManager navManager; // Reference to the NavigationManager script

    void Start()
    {

        GameObject navManagerObject = GameObject.FindGameObjectWithTag("NavManager");
        if (navManagerObject != null)
        {
            NavigationManager navManager = navManagerObject.GetComponent<NavigationManager>();
            if (navManager != null)
            {
                // Now you can use navManager as a NavigationManager instance
                this.navManager = navManager;
            }
            else
            {
                Debug.LogError("NavigationManager component not found on the GameObject tagged as 'NavManager'.");
            }
        }
        else
        {
            Debug.LogError("No GameObject found with the tag 'NavManager'.");
        }

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

        if (navManager == null)
        {
            // Get NavigationManager dynamically if not assigned in the Inspector
            navManager = GetComponent<NavigationManager>();
            if (navManager == null)
            {
                Debug.LogError("NavigationManager component is not found on this GameObject!");
                return;
            }
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
        navManager.SetDestination(selectedRoom);

        // Optionally load the navigation scene
        SceneManager.LoadScene("NavScene");
    }
}