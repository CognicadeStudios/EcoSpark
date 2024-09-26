using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraPan : MonoBehaviour
{
    // Set the min and the max size for this 
    // camera so that we can fix the limits
    // of our zoom in and zoom out.
    [SerializeField]
    float CameraSizeMin = 1.0f;
    [SerializeField]
    float CameraSizeMax = 10.0f;
    // The slider for zoom-in and zoom-out
    [SerializeField]
    float CameraMoveSpeed = 5.0f, CameraZoomSpeed = 5.0f;

    // Some variables needed for dragging our 
    // camera to creae the pan control
    private Vector3 mDragPos;
    private Vector3 mOriginalPosition;
    private bool mDragging = false;
    // The zoom factor
    private float mZoomFactor = 1.0f;
    // Save a reference to the Camera.main
    private Camera mCamera;

    // A property to allow/disallow camera panning
    public static bool IsCameraPanning
    {
        get;
        set;
    } = true;

    void Start()
    {
        if(CameraSizeMax < CameraSizeMin)
        {
            float tmp = CameraSizeMax;
            CameraSizeMax = CameraSizeMin;
            CameraSizeMin = tmp;
        }
        if(CameraSizeMax - CameraSizeMin < 0.01f)
        {
            CameraSizeMax += 0.1f;
        }

        SetCamera(Camera.main);
    } 
    public void SetCamera(Camera camera)
    { 
        mCamera = camera;
        mOriginalPosition = mCamera.transform.position;
        // For this demo, we simple take the current camera
        // and calculate the zoom factor.
        // Alternately, you may want to set the zoom factor
        // in other ways. For example, randomize the 
        // zoom factory between 0 and 1.
        mZoomFactor = 
          (CameraSizeMax - mCamera.orthographicSize) / 
          (CameraSizeMax - CameraSizeMin);
        
    }

    public void Zoom(float value)
    {
        mZoomFactor = value;
        // clamp the value between 0 and 1.
        mZoomFactor = Mathf.Clamp01(mZoomFactor);
        // set the camera size
        mCamera.orthographicSize = CameraSizeMax -
            mZoomFactor * 
            (CameraSizeMax - CameraSizeMin);
    }
    public void ZoomIn()
    {
        Zoom(mZoomFactor + 0.01f);
    }
    public void ZoomOut()
    {
        Zoom(mZoomFactor - 0.01f);
    }

    void Update()
    {
        // Camera panning is disabled when a tile is selected.
        if (!IsCameraPanning)
        {
            mDragging = false;
            return;
        }
        // We also check if the pointer is not on UI item
        // or is disabled.
        if (EventSystem.current.IsPointerOverGameObject() || enabled == false)
        {
            mDragging = false;
            return;
        }
        // Save the position in worldspace.
        if (Input.GetMouseButtonDown(0))
        {
            mDragPos = Input.mousePosition;
            mDragging = true;
        }
        if (Input.GetMouseButton(0) && mDragging)
        {
            Vector3 diff = mDragPos - Input.mousePosition;
            diff.z = diff.y;
            diff.y = 0.0f;

            //account for rotation of camera 
            Vector3 rotatedDif = Quaternion.Euler(0, mCamera.transform.eulerAngles.y, 0) * diff;
            
            mCamera.transform.position += rotatedDif * Time.deltaTime * CameraMoveSpeed;
            mDragPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mDragging = false;
        }

        float scrollAxis = Input.mouseScrollDelta.y * -1.0f;
        Zoom(mZoomFactor + (scrollAxis * Time.deltaTime * CameraZoomSpeed));
    }
}   
