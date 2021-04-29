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
    public GameObject Item;
    private GameObject spawnedItem;
    [SerializeField]
    private CareManager careManager;
    private GameObject creature;
    private bool isCreature = false;
    private GameObject creatureSpawned;
    [SerializeField]
    private CareUIButton UIbutton;
    private bool isFeedMode = false;
    
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        indicator = transform.GetChild(0).gameObject; //첫번째 자식
        indicator.SetActive(false);
        creature = careManager.GetCreatureObject();
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


    private void OnCollisionEnter(Collision other) 
    {
        if(other.transform.tag == "food")
        {
            Destroy(other.gameObject);
            careManager.FeedIt(10f);
        }    
    }

    public void SetItem(GameObject item)
    {
        this.Item = item;
    }

    public void SetIsFeedMode(bool feedMode)
    {
        this.isFeedMode = feedMode;
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
            /*
            if(spawned != null)
            {
                spawned.transform.position = transform.position;
                spawned.transform.rotation = transform.rotation;
            }
            */
            if(!isCreature){
                creatureSpawned = Instantiate(creature, transform.position, transform.rotation);
                careManager.SetMyCreature(creatureSpawned);
                isCreature = true;
            }

            if(creatureSpawned != null)
            {
                creatureSpawned.transform.position = transform.position;
                creatureSpawned.transform.rotation = transform.rotation;
            }

        }
        else
        {
            indicator.SetActive(false);
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(isFeedMode){
                // create ray from the camera at the mouse position
                //Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(rayManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    spawnedItem = Instantiate(Item, hitPose.position, hitPose.rotation);
                    UIbutton.useItem();
                }    
            }
        }
        
    }
}
