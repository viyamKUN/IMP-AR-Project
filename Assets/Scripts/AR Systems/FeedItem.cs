using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class FeedItem : MonoBehaviour
{
    public GameObject FoodItem;
    private GameObject food;
    private ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawned;


    public void AddFoodItem()
    {
        food = Instantiate(FoodItem);
    }

    public void Update()
    {
        //if(TryGetTouchPosition(out Vector2 touchPosition))
        if(Input.GetMouseButtonDown(0))
        {
            // create ray from the camera at the mouse position
            //Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                spawned = Instantiate(FoodItem, hitPose.position + new Vector3(0, 0.5f, 0), hitPose.rotation);
                Debug.Log("test");
                Debug.Log(spawned);
            }
            
        }    


    }
    
}
