using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class AirCraftController : MonoBehaviour {

    public float speed = 100f;
    public float speedLimit = 800f;
    public float rollConstant = 5f;
    public float pitchConstant = 5f;
    public float normalAngularDrag = 2f;
    public float emergAngularDrag = 10f;
    public float angSpeedLimitSqr = 10f;
    public float normalDrag = 1f;
    public float emergencyDrag = 5f;
    private bool getControl;
    private float elapedTime;
    private float delayTime;
    // Acceleration
    private Vector3 zeroAcc;
    private Vector3 curAcc;
    private float sensorHorizon = 10f;
    private float sensorVertical = 10f;
    private float smooth = 0.5f;
    private float axisHorizon = 0;
    private float axisVertical = 0;

    // Use this for initialization
    void Start () {
        rigidbody.drag = normalDrag;
        rigidbody.angularDrag = normalAngularDrag;
        elapedTime = 0;
        getControl = false;
        delayTime = 1.0f;
        ResetAccelometer ();
    }

    void ResetAccelometer() {
        zeroAcc = Input.acceleration;
        curAcc = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!getControl) {
            elapedTime += Time.deltaTime;
            if (elapedTime < delayTime) {
                return;
            } else {
                getControl = true;
                return;
            }
        }

        ApplySpeedLimits();
//        HandleMouse();
        HandleAccelometer ();
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

        rigidbody.AddRelativeTorque(Vector3.up * roll * rollConstant);
        rigidbody.AddRelativeTorque(Vector3.left * pitch * pitchConstant);
        
        Stabilize();
    }

    private void HandleAccelometer() {
        curAcc = Vector3.Lerp (curAcc, Input.acceleration - zeroAcc, Time.deltaTime / smooth);
        axisVertical = Mathf.Clamp(curAcc.y * sensorVertical, -1, 1);
        axisHorizon = Mathf.Clamp(curAcc.x * sensorHorizon, -1, 1);


        rigidbody.AddRelativeTorque(Vector3.up * axisHorizon/* * rollConstant*/);
        rigidbody.AddRelativeTorque(Vector3.left * axisVertical/* * pitchConstant*/);
        
        //Stabilize();
    }
}
