using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public Texture2D shootBtn;
	public WeaponGun gun;
	public WeaponMissile missile;
    Vector3 targetPoint;
    Ray raycast;
    Plane playerPlane;
    bool explodePrepare;
    float hitDist;
    public GameObject bullet;

    // Use this for initialization
    void Start () {
        playerPlane = new Plane (Vector3.up, transform.position);
        explodePrepare = false;
        hitDist = 0;
    }

    void Update() {

        if (Input.touchCount == 2) {
            Touch touch0 =  Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Stationary && touch1.phase == TouchPhase.Began) {
                // Generate a plane that intersects the transform's position with an upwards normal.
                raycast = Camera.main.ScreenPointToRay(Input.touches[1].position);
                explodePrepare = true;
            }
        }

        if (Input.GetMouseButtonDown (0)) {
            raycast = Camera.main.ScreenPointToRay (Input.mousePosition);
            explodePrepare = true;
        }

        if (explodePrepare) {

            if (playerPlane.Raycast (raycast, out hitDist)) {
                // Get the point along the ray that hits the calculated distance.
                targetPoint = raycast.GetPoint(hitDist);
                // Instantiate the Weapon here
                Instantiate(bullet, targetPoint, Quaternion.identity);
            }
            explodePrepare = false;
        }
    }
}
