using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class AirCraftController : MonoBehaviour {

    public float speed = 10000f;
    public float speedLimit = 30000f;
    public float normalAngularDrag = 2f;
    public float emergAngularDrag = 10f;
    public float angSpeedLimitSqr = 10f;
    public float normalDrag = 1f;
    public float emergencyDrag = 5f;
    private bool getControl;
    private float elapedTime;
    private float delayTime;

    // Acceleration
    private Vector3 lastAcc;
    private Vector3 curAcc;
    private Vector3 handler;
    private float smooth = 0.5f;

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
        lastAcc = Input.acceleration;
        curAcc = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate () {
        // Wait a second for user to see the scene
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
        HandleSwipe ();
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

        rigidbody.AddRelativeTorque(Vector3.up * roll * 10f);
        rigidbody.AddRelativeTorque(Vector3.left * pitch * 10f);
        
//        Stabilize();
    }

    private void HandleSwipe() {
        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {

            // Get movement of the finger since last frame
            Vector2 touchDeltaPos = Input.GetTouch(0).deltaPosition;
            Vector3 thrust = Vector3.zero;

            thrust += new Vector3(-speed * touchDeltaPos.x * Time.deltaTime, 0, 0);
            thrust += new Vector3(0, 0, -speed * touchDeltaPos.y * Time.deltaTime);

            rigidbody.AddRelativeForce(thrust);
        }
    }

    private void HandleAccelometer() {
        curAcc = Vector3.Lerp (curAcc, Input.acceleration - lastAcc, Time.deltaTime / smooth);

        handler.x = Mathf.Clamp(curAcc.x * 10f, -1, 1);
        handler.y = Mathf.Clamp(curAcc.y * 10f, -1, 1);
        handler.z = Mathf.Clamp(curAcc.z * 10f, -1, 1);

        Vector3 torque = Vector3.zero;
        torque += new Vector3 (handler.x * 10f, 0, 0);
        torque += new Vector3(0, handler.y * 10f, 0);
        torque += new Vector3(0, 0, handler.z * 10f);   

        rigidbody.AddRelativeTorque (torque);

        lastAcc = Input.acceleration;

        //Stabilize();
    }

}
