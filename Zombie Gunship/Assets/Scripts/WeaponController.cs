using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public Texture2D shootBtn;
	public WeaponGun gun;
	public WeaponMissile missile;
    Detonator detonator;

    // Use this for initialization
    void Start () {
        detonator = gameObject.GetComponent<Detonator> ();
	}

    void Update() {
        if (Input.touchCount == 2) {
            Touch touch0 =  Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Stationary && touch1.phase == TouchPhase.Began) {
                // Generate a plane that intersects the transform's position with an upwards normal.
                Plane playerPlane = new Plane (Vector3.up, transform.position);
                // Ray raycast = Camera.main.ScreenPointToRay (Input.mousePosition);
                Ray raycast = Camera.main.ScreenPointToRay(Input.touches[1].position);
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
}
