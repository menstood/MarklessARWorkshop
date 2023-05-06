using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlaceObject : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager raycastManager;
    [SerializeField]
    private ARPlaneManager planeManager;
    [SerializeField]
    private ARSessionOrigin sessionOrigin;
    [SerializeField]
    private GameObject arObjectToPlace;
    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private MeshRenderer indicatorMesh;

    private bool validPlace;

    void Start()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += PlaceAR;
    }

    void PlaceAR(Finger finger)
    {
        if (validPlace)
        {
            Instantiate(arObjectToPlace,indicator.transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        UpdateIndicator();
    }
    void UpdateIndicator()
    {
        var screenCenter = sessionOrigin.camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        validPlace = hits.Count > 0;
        indicatorMesh.material.color = validPlace ? Color.green : Color.red;
        if (validPlace)
        indicator.transform.position = hits[0].pose.position;
    }
}
