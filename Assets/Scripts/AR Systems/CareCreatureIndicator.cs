using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

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
    private bool isFeedMode = false;
    int selectedItemNumber = 0;
    private ARPlacementInteractable placementInteractable;
    private ARSessionOrigin arSessionOrigin;
    void Start()
    {
        rayManager = FindObjectOfType<ARRaycastManager>();
        indicator = transform.GetChild(0).gameObject; 
        indicator.SetActive(false);
        creature = careManager.GetCreatureObject();
        placementInteractable = GetComponent<ARPlacementInteractable>();
        arSessionOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    public void SetItem(GameObject item)
    {
        this.Item = item;
    }

    public void SetIsFeedMode(bool feedMode, int id)
    {
        this.isFeedMode = feedMode;
        selectedItemNumber = id;
    }

    // Update is called once per frame
    public void Update()
    {
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon);
        if (hits.Count > 0)
        {

            if (!indicator.activeInHierarchy)
            {
                indicator.SetActive(true);
            }

            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!isSpawned)
            {
                spawned = Instantiate(ObjectToSpawn, transform.position, transform.rotation);
                isSpawned = true;
            }
            
            if (!isCreature)
            {
                creatureSpawned = Instantiate(creature, transform.position, transform.rotation);
                creatureSpawned.transform.parent = arSessionOrigin.trackablesParent;
                careManager.SetMyCreature(creatureSpawned);
                creatureSpawned.GetComponent<ARAnnotationInteractable>().enabled = true;             
                creatureSpawned.GetComponent<ARScaleInteractable>().enabled = true;
                creatureSpawned.GetComponent<ARRotationInteractable>().enabled = true;
                creatureSpawned.GetComponent<ARSelectionInteractable>().enabled = true;
                isCreature = true;
            }

            if (creatureSpawned != null)
            {
                creatureSpawned.transform.position = transform.position;
                creatureSpawned.transform.rotation = transform.rotation;
            }

        }
        else
        {
            indicator.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isFeedMode)
            {
                // create ray from the camera at the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (rayManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    spawnedItem = Instantiate(Item, hitPose.position, hitPose.rotation);
                    careManager.UseItem(selectedItemNumber, 1, out int remain);
                    if (remain == 0)
                        SetIsFeedMode(false, -1);
                }
            }
        }

    }
}
