using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public WeaponGun gun;
	public WeaponMissile missile;
    Detonator detonator;

    // Use this for initialization
    void Start () {
        detonator = gameObject.GetComponent<Detonator> ();
	}
	
	// Update is called once per frame
	void Update () {

//        if (Input.GetMouseButtonDown (0)) {
//            // Bullet shoot
//        } else if (Input.GetMouseButtonUp (0)) {
//            // Bullet stop
//        }
//
//        if (Input.GetMouseButtonDown (1)) {
//            // Canon shoot
//        } else if (Input.GetMouseButtonUp (1)) {
//            // Canon stop
//        }
//
//        if (Input.GetMouseButtonDown (2)) {
//            // Missile shoot
//        } else if (Input.GetMouseButtonUp (2)) {
//            // Missile stop
//        }

        if (Input.GetMouseButtonDown(0)) {
            // Generate a plane that intersects the transform's position with an upwards normal.
            Plane playerPlane = new Plane (Vector3.up, transform.position);
            Ray raycast = Camera.main.ScreenPointToRay (Input.mousePosition);
            float hitDist = 0;

            if (playerPlane.Raycast (raycast, out hitDist)) {
                // Get the point along the ray that hits the calculated distance.
                Vector3 targetPoint = raycast.GetPoint(hitDist);
                detonator.gameObject.transform.position = targetPoint;
                detonator.Explode();
            }
        }
	}
}
