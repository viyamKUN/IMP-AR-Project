using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImage : MonoBehaviour
{
    private ARTrackedImageManager trackedManager;
    public GameObject gameObjectInstantiate;
    private GameObject spawned;
    public GameManager gameManager;

    private void Awake()
    {
        trackedManager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        //subscribing to the trackedImageChange event
        trackedManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        trackedManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage img in args.added)
        {
            if (spawned == null)
            {
                spawned = Instantiate(gameObjectInstantiate, img.transform.position, img.transform.rotation);
                gameManager.SetBoxPosition(img.transform.position);
            }
        }
        foreach (ARTrackedImage img in args.updated)
        {
            spawned.transform.position = img.transform.position;
            //spawned.transform.rotation = img.transform.rotation;
            gameManager.SetBoxPosition(img.transform.position);
            spawned.SetActive(true);
        }
        foreach (ARTrackedImage img in args.removed)
        {
            gameManager.SetBoxPosition(img.transform.position);
            spawned.SetActive(false);
        }
    }
}
