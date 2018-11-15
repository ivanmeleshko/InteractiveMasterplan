using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    //public GameObject camera_GameObject;

    //Vector2 StartPosition;
    //Vector2 DragStartPosition;
    //Vector2 DragNewPosition;
    //Vector2 Finger0Position;
    //float DistanceBetweenFingers;
    //bool isZooming;


    //private void Update()
    //{
    //    if (Input.touchCount == 0 && isZooming)
    //    {
    //        isZooming = false;
    //    }

    //    if (Input.touchCount == 1)
    //    {
    //        if (!isZooming)
    //        {
    //            if (Input.GetTouch(0).phase == TouchPhase.Moved)
    //            {
    //                Vector2 NewPosition = GetWorldPosition();
    //                Vector2 PositionDifference = NewPosition - StartPosition;
    //                camera_GameObject.transform.Translate(-PositionDifference);
    //            }
    //            StartPosition = GetWorldPosition();
    //        }
    //    }
    //    else if (Input.touchCount == 2)
    //    {
    //        if (Input.GetTouch(1).phase == TouchPhase.Moved)
    //        {
    //            isZooming = true;

    //            DragNewPosition = GetWorldPositionOfFinger(1);
    //            Vector2 PositionDifference = DragNewPosition - DragStartPosition;
    //            if (Vector2.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
    //                camera_GameObject.GetComponent<Camera>().orthographicSize += (PositionDifference.magnitude);
    //            if (Vector2.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
    //                camera_GameObject.GetComponent<Camera>().orthographicSize -= (PositionDifference.magnitude);

    //            DistanceBetweenFingers = Vector2.Distance(DragNewPosition, Finger0Position);
    //        }
    //        DragStartPosition = GetWorldPositionOfFinger(1);
    //        Finger0Position = GetWorldPositionOfFinger(0);
    //    }
    //}


    //Vector2 GetWorldPosition()
    //{
    //    return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    //}


    //Vector2 GetWorldPositionOfFinger(int FingerIndex)
    //{
    //    return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(FingerIndex).position);
    //}
    private static readonly float PanSpeed = 200f;
    private static readonly float ZoomSpeedTouch = 0.1f;
    private static readonly float ZoomSpeedMouse = 0.5f;

    private static readonly float[] BoundsX = new float[] { -1000f, 500f };
    private static readonly float[] BoundsZ = new float[] { -1800f, -400f };
    private static readonly float[] ZoomBounds = new float[] { 0.1f, 85f };

    private Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId; // Touch mode only

    private bool wasZoomingLastFrame; // Touch mode only
    private Vector2[] lastZoomPositions; // Touch mode only

    public float speed;


    void Awake()
    {
        cam = GetComponent<Camera>();
    }


    void Update()
    {
        //transform.Rotate(0, speed * Time.deltaTime, 0);
        if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
        {
            HandleTouch();
        }
        else
        {
            HandleMouse();
        }

    }


    void HandleTouch()
    {
        switch (Input.touchCount)
        {

            case 1: // Panning
                wasZoomingLastFrame = false;
                RotateBySwipe();
                //// If the touch began, capture its position and its finger ID.
                //// Otherwise, if the finger ID of the touch doesn't match, skip it.
                //Touch touch = Input.GetTouch(0);
                //if (touch.phase == TouchPhase.Began)
                //{
                //    lastPanPosition = touch.position;
                //    panFingerId = touch.fingerId;
                //}
                //else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                //{
                //    PanCamera(touch.position);
                //}
                break;

                

            case 2: // Zooming
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame)
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else
                {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, ZoomSpeedTouch);

                    lastZoomPositions = newPositions;
                }
                break;

            default:
                wasZoomingLastFrame = false;
                break;
        }
    }


    void HandleMouse()
    {
        // On mouse down, capture it's position.
        // Otherwise, if the mouse is still down, pan the camera.
        if (Input.GetMouseButtonDown(0))
        {
            lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }

        // Check for scrolling to zoom the camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, ZoomSpeedMouse);
    }


    void PanCamera(Vector3 newPanPosition)
    {
        // Determine how much to move the camera
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, 0, offset.y * PanSpeed);

        // Perform the movement
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);
        transform.position = pos;

        // Cache the position
        lastPanPosition = newPanPosition;
    }


    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;


 

    void RotateBySwipe()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                FirstPoint = Input.GetTouch(0).position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                SecondPoint = Input.GetTouch(0).position;
                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                //this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
				if (xAngle > 0)
				{
					transform.Rotate(0, 15, 0);
				}
				else if (xAngle < 0)
				{
					transform.Rotate(0, -15, 0);
				}
            }
        }
    }



    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        {
            return;
        }

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }
}