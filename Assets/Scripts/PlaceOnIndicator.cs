using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnIndicator : MonoBehaviour
{
    [SerializeField]
    GameObject placementIndicator;
    [SerializeField]
    GameObject placedPrefab;
    GameObject spawnedObject;

    public bool placedMarkerstatus = false;
    public bool placedModelStatus = false;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [SerializeField]
    DownloadAssetBundle downloadasset;

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        placementIndicator.SetActive(false);

    }

    private void Start()
    {
        downloadasset = GetComponent<DownloadAssetBundle>();
    }

    private void Update()
    {
        if(aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpose = hits[0].pose;
            placementIndicator.transform.SetPositionAndRotation(hitpose.position, hitpose.rotation);

            if(!placementIndicator.activeInHierarchy && placedMarkerstatus)
            {
                placementIndicator.SetActive(true);
            }
            else if(placementIndicator.activeInHierarchy && ! placedMarkerstatus)
            {
                placementIndicator.SetActive(false);
            }
        }
    }

    public void EnableDisablePlacedMarker()
    {
        if (!placedMarkerstatus)
        {
        placedMarkerstatus = true;
        }
        else
        placedMarkerstatus = false;
    }

    public void PlacedObject()
    {
        if (!placementIndicator.activeInHierarchy)
            return;

        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(downloadasset.InstantiateGameObjFromServer(), placementIndicator.transform.position, placementIndicator.transform.rotation);
        }
        else
        {
            spawnedObject.transform.SetPositionAndRotation(placementIndicator.transform.position, placementIndicator.transform.rotation);
        }
    }

}
