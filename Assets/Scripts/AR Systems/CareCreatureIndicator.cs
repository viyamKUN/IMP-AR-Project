using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CareCreatureIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;
    private GameObject indicator;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isSpawned = false;
    private GameObject spawned;
    public GameObject ObjectToSpawn;
    private Vector3 indicatorPosition;
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        indicator = transform.GetChild(0).gameObject; //첫번째 자식
        indicator.SetActive(false);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        rayManager.Raycast(new Vector2(Screen.width/2, Screen.height/2), hits, TrackableType.PlaneWithinPolygon);
        if(hits.Count > 0)
        {
            
            if(!indicator.activeInHierarchy)
            {
                indicator.SetActive(true);
            }

            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            
            indicator.transform.Rotate(new Vector3(0, 20f, 0) * Time.deltaTime);

            if(spawned != null)
            {
                spawned.transform.position = transform.position;
                spawned.transform.rotation = transform.rotation;
            }
        }
        else
        {
            indicator.SetActive(false);
        }

        if(TryGetTouchPosition(out Vector2 touchPosition))
        {
            if(spawned != null)
            {
                spawned.transform.position = transform.position;
            }
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            if(rayManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                if(!isSpawned)
                {
                    spawned = Instantiate(ObjectToSpawn, transform.position, transform.rotation);
                    isSpawned = true;
                }
                else
                {
                    /*
                    transform.position = hitPose.position;
                    spawned.transform.position = transform.position;
                    spawned.transform.rotation = transform.rotation;
                    */
                }
            }
        }
    }
}
