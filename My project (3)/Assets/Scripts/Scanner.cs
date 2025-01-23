using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class Scanner : MonoBehaviour
{
    [SerializeField]
ARTrackedImageManager m_TrackedImageManager;

public static Vector3 position;

public static Quaternion rotation;

public static String imageName;

private XROrigin xrOrigin;

void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
{
    foreach (var newImage in eventArgs.added)
        {
            imageName = newImage.referenceImage.name;
            Debug.Log($"Updated Image Name: {imageName}"); 


        }
    
    SceneManager.LoadScene("NavScene");

    foreach (var updatedImage in eventArgs.updated)
    {
        // Handle updated event
    }

    foreach (var removedImage in eventArgs.removed)
    {
        // Handle removed event
    }
}
}
