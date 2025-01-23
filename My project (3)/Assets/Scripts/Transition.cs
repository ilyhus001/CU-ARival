using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;

public class PersistentARSessionManager : MonoBehaviour
{
    public static PersistentARSessionManager Instance { get; private set; }

    [SerializeField] private ARSession arSession;
    [SerializeField] private XROrigin arSessionOrigin;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Ensure AR session persists between scenes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reconnect AR session if needed
        if (arSession != null)
        {
            arSession.enabled = true;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}