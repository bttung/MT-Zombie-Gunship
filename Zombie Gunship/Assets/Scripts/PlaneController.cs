using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {

    public GameObject bullet;
    private Transform weapon;
    private Transform bulletSpawnPoint;
    private float curSpeed;
    private float targetSpeed;
    private float rotSpeed;
    private float weaponRotSpeed = 10.0f;
    private float maxForwardSpeed = 300.0f;
    private float maxBackwardSpeed = -300.0f;

    // Bullet Shooting Rate
    protected float shootRate = 0.5f;
    protected float elapasedTime;

	// Use this for initialization
	void Start () {
        // Plane Setting
        rotSpeed = 150.0f;

        bulletSpawnPoint = transform;
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateWeapon ();
        UpdateControl ();
	}

    void UpdateWeapon() {
        if (Input.GetMouseButtonDown (0)) {
            elapasedTime += Time.deltaTime;
            if (elapasedTime >= shootRate) {
                // Reset the time
                elapasedTime = 0.0f;

                // Instantiate the bullet
                Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
    }

    void UpdateControl() {
        // Aiming with the mouse
        // Generate a plane that intersects the transform's position with an upward normal
        Plane playerPlane = new Plane(Vector3.up, transform.position + new Vector3(0, 0, 0));

        // Generate a ray from the cursor position
        Ray raycast = Camera.main.ScreenPointToRay (Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        float hitDist = 0;

        // If the ray is paralle to the plane, raycast will return false
        if (playerPlane.Raycast (raycast, out hitDist)) {
            // Get the point along the ray that hits the calculated distance
            Vector3 rayHitPoint = raycast.GetPoint(hitDist);

            Quaternion targetRotation = Quaternion.LookRotation(rayHitPoint - transform.position);
            weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, targetRotation, Time.deltaTime * weaponRotSpeed);
        }
    }
}
