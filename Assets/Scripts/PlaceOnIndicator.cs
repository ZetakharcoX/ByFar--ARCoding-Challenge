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
    GameObject spawnedObject;

    public bool placedMarkerStatus = false;
    public bool placedModelStatus = false;

    ARRaycastManager arrayCastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    DownloadAssetBundle downLoadAsset;

    private void Awake()
    {
        arrayCastManager = GetComponent<ARRaycastManager>();
        placementIndicator.SetActive(false);

    }

    private void Start()
    {
        downLoadAsset = GetComponent<DownloadAssetBundle>();
    }

    private void Update()
    {
        if(arrayCastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpose = hits[0].pose;
            placementIndicator.transform.SetPositionAndRotation(hitpose.position, hitpose.rotation);

            if(!placementIndicator.activeInHierarchy && placedMarkerStatus)
            {
                placementIndicator.SetActive(true);
            }
            else if(placementIndicator.activeInHierarchy && ! placedMarkerStatus)
            {
                placementIndicator.SetActive(false);
            }
        }
    }

    public void EnableDisablePlacedMarker()
    {
        if (!placedMarkerStatus)
        {
        placedMarkerStatus = true;
        }
        else
        placedMarkerStatus = false;
    }

    public void PlacedObject()
    {
        if (!placementIndicator.activeInHierarchy)
            return;

        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(downLoadAsset.InstantiateGameObjFromServer(), placementIndicator.transform.position, placementIndicator.transform.rotation);
        }
        else
        {
            spawnedObject.transform.SetPositionAndRotation(placementIndicator.transform.position, placementIndicator.transform.rotation);
        }
    }

}
