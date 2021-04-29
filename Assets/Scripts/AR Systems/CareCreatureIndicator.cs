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
    public GameObject FoodItem;
    private GameObject food;
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

    public void feedItem()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // create ray from the camera at the mouse position
            //Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            /*
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(rayManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                food = Instantiate(FoodItem, hitPose.position, hitPose.rotation);
            }
            */
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 touchPos = new Vector2(pos.x, pos.y);

            food = Instantiate(FoodItem, touchPos, Quaternion.identity);
            Debug.Log(touchPos);
            Debug.Log(food.transform.position);

            
        }  
    }

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("collllllllllll");
        if(other.transform.tag == "food")
        {
            Debug.Log("collide!");
            Destroy(other.gameObject);
        }    
    }

    // Update is called once per frame
    public void Update()
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
            
            //indicator.transform.Rotate(new Vector3(0, 20f, 0) * Time.deltaTime);
            if(!isSpawned){
                spawned = Instantiate(ObjectToSpawn, transform.position, transform.rotation);
                isSpawned = true;
            }

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

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("touch");
            // create ray from the camera at the mouse position
            //Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(rayManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                food = Instantiate(FoodItem, hitPose.position, hitPose.rotation);
                
            }
            
        }
        
    }
}
