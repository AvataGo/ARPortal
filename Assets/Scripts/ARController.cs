using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARController : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public GameObject Portal;
    public GameObject ARCamera;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();        
    }

    void Update()
    {
        Touch touch;
        if(Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        if(raycastManager.Raycast(new Vector2(touch.position.x, touch.position.y), hits, TrackableType.PlaneWithinPolygon))
        {
            Portal.SetActive(true);

            Portal.transform.position = hits[0].pose.position;
            Portal.transform.rotation = hits[0].pose.rotation;

            Vector3 ARCameraPostion = ARCamera.transform.position;

            ARCameraPostion.y = hits[0].pose.position.y;

            Portal.transform.LookAt(ARCameraPostion, Portal.transform.up);            

        }
        
    }
}
