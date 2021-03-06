﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class AirCraftController : MonoBehaviour {

    public float speed = 50000f;
    public float speedLimit = 80000f;
    public float normalAngularDrag = 2f;
    public float emergAngularDrag = 10f;
    public float angSpeedLimitSqr = 10f;
    public float normalDrag = 1f;
    public float emergencyDrag = 5f;
    private float elapedTime;
    private float delayTime;

    // Acceleration
    private Vector3 lastAcc;
    private Vector3 curAcc;
    private Vector3 handler;
    private float smooth = 0.5f;

    // Camera Zoom
    private int cameraZoomSpeed = 4;
    private float MIN_SCALE = 2.0f;
    private float MAX_SCALE = 5.0f;
    private float minPinchSpeed = 5.0f;
    private float variancesDist = 5.0f;
    private float touchDelta = 0.0f;
    private float speedTouch0 = 0.0f;
    private float speedTouch1 = 0.0f;

    // Use this for initialization
    void Start () {
        rigidbody.drag = normalDrag;
        rigidbody.angularDrag = normalAngularDrag;
        elapedTime = 0;
        delayTime = 1.0f;
        ResetAccelometer ();
    }

    void ResetAccelometer() {
        lastAcc = Input.acceleration;
        curAcc = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate () {
        ApplySpeedLimits();

//      HandleMouse();
        HandleSwipe ();
        HandleZoom ();
    }

    void Update() {
        // This one is for rotate the camera by Accelometer
        elapedTime += Time.deltaTime;
        if (elapedTime < 10.0f) {
        
        } else {
//            SmartPhone cannot afford this process
//            HandleAccelometer ();
            elapedTime = 0;
        }
    }
    
    private void ApplySpeedLimits() {
        // Apply speed limit
        if (rigidbody.velocity.sqrMagnitude > speedLimit) {
            rigidbody.drag = emergencyDrag;
        }
        else {
            rigidbody.drag = normalDrag;
        }
        
        // Apply rotation limit
        if (rigidbody.angularVelocity.sqrMagnitude > angSpeedLimitSqr) {
            rigidbody.angularDrag = emergAngularDrag;   
        }
        else {
            rigidbody.angularDrag = normalAngularDrag;  
        }
    }
    
    private void Stabilize() {        
        Vector3 torqueVector = Vector3.Cross(rigidbody.transform.up, Vector3.up);
        torqueVector = Vector3.Project(torqueVector, transform.forward);
        
        rigidbody.AddTorque(torqueVector * Time.deltaTime * 150);
    }


    private void HandleMouse() {    
        float roll = Input.GetAxis("Mouse X");
        float pitch = Input.GetAxis("Mouse Y");

        rigidbody.AddRelativeTorque(Vector3.up * roll * 10f);
        rigidbody.AddRelativeTorque(Vector3.left * pitch * 10f);
        
        Stabilize();
    }

    private void HandleZoom()  {
        Vector2 prevDist = new Vector2 (0, 0);
        Vector2 curDist = new Vector2 (0, 0);

        if (Input.touchCount == 2 && Input.GetTouch (0).phase == TouchPhase.Moved && Input.GetTouch (1).phase == TouchPhase.Moved) {
            curDist = Input.GetTouch(0).position - Input.GetTouch(1).position;
            prevDist = (Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);
            touchDelta = curDist.magnitude - prevDist.magnitude;
            speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
            speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;

            if ((touchDelta + variancesDist <= 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed)) {                
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + (1 * cameraZoomSpeed), 7, 90);
            }
            
            if ((touchDelta +variancesDist > 1) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed)) {
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - (1 * cameraZoomSpeed), 7, 90);
            }
        }
    }
        
    private void HandleSwipe() {
        if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved) {

            // Get movement of the finger since last frame
            Vector2 touchDeltaPos = Input.GetTouch(0).deltaPosition;
            Vector3 thrust = Vector3.zero;

            thrust += new Vector3(-speed * touchDeltaPos.x * Time.deltaTime, 0, 0);
            thrust += new Vector3(0, -speed * touchDeltaPos.y * Time.deltaTime, 0);

            rigidbody.AddRelativeForce(thrust);
        }
    }

    private void HandleAccelometer() {
        curAcc = Vector3.Lerp (curAcc, Input.acceleration - lastAcc, Time.deltaTime / smooth);

        handler.x = Mathf.Clamp(curAcc.x * 10f, -1, 1);
        handler.y = Mathf.Clamp(curAcc.y * 10f, -1, 1);
        handler.z = Mathf.Clamp(curAcc.z * 10f, -1, 1);

        Vector3 torque = Vector3.zero;
        torque += new Vector3 (handler.x * 3, 0, 0);
        torque += new Vector3(0, handler.y * 3, 0);
        torque += new Vector3(0, 0, handler.z * 3);   

        rigidbody.AddRelativeTorque (torque);

        lastAcc = Input.acceleration;

        //Stabilize();
    }

}
