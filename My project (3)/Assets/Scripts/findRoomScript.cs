using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class findRoomScript : MonoBehaviour
{
    public Button Button5201;
    public Button Button5107;
    //public TMP_Dropdown dropDown;
    private static string destination;
    private Button clickedButton; 
    public TMP_Text locationText;

    void Start()
    {
        if (Button5201 == null)
        {
            Debug.LogError("Button5201 is not assigned!");
            return;
        }

        if (Button5107 == null)
        {
            Debug.LogError("Button5107 is not assigned!");
            return;
        }

        Button5201.onClick.AddListener(() => OnNavigateButtonClicked(Button5201));
        Button5107.onClick.AddListener(() => OnNavigateButtonClicked(Button5107));
    }

    public string GetRoom()
    {
        if (clickedButton == null)
        {
            Debug.LogWarning("No button has been clicked");
            return null;
        }

        TextMeshProUGUI textComponent = clickedButton.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            return textComponent.text; 
        }

        Debug.LogError("No TextMeshProUGUI component found lol");
        return null;
    }

    private void OnNavigateButtonClicked(Button button)
    {
        clickedButton = button; 
        string selectedRoom = GetRoom();
        Debug.Log("Selected Room: " + selectedRoom);

        // Pass the selected room to the NavigationManager
        SetDestination(selectedRoom.Replace(" ", "").Trim());
        locationText.text = "Navigating to room" + selectedRoom;

        // Optionally load the navigation scene
        SceneManager.LoadScene("NavScene");
    }

    public void SetDestination(string roomName)
    {
        destination = roomName;
    }

    public static string GetDestination()
    {
        return destination;
    }
}
