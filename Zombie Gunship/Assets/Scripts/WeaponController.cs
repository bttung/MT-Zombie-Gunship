using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    Ray raycast;
    float hitDist;
    Vector3 targetPoint;
    Plane playerPlane;
    bool explodePrepare;
    private float elapsedShootTime;
    private float reloadTime;

    private int weaponType;
    public GameObject weapon;
    public GameObject bullet;
    public GameObject canon;
    public GameObject missile;
    public Texture2D bulletBtn;
    public Texture2D canonBtn;
    public Texture2D missileBtn;


    // Use this for initialization
    void Start () {
        playerPlane = new Plane (Vector3.up, transform.position);
        explodePrepare = false;
        hitDist = 0;
        reloadTime = weapon.gameObject.GetComponent<Weapon> ().loadTime;
        elapsedShootTime = 0;
        weaponType = 1;
        SetWeaponType (1);
    }

    void Update() {

        elapsedShootTime += Time.deltaTime;
        if (elapsedShootTime < reloadTime) {
            return;
        }

        if (Input.touchCount == 2) {
            Touch touch0 =  Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Stationary && touch1.phase == TouchPhase.Began) {
                // Generate a plane that intersects the transform's position with an upwards normal.
                raycast = Camera.main.ScreenPointToRay(Input.touches[1].position);
                explodePrepare = true;
            }
        }

        // This one is for PC
//        if (Input.GetMouseButtonDown (0)) {
//            raycast = Camera.main.ScreenPointToRay (Input.mousePosition);
//            explodePrepare = true;
//        }

        if (explodePrepare) {
            if (playerPlane.Raycast (raycast, out hitDist)) {
                // Get the point along the ray that hits the calculated distance.
                targetPoint = raycast.GetPoint(hitDist);               

                // Instantiate the Weapon here
                Instantiate(bullet, targetPoint, Quaternion.identity);
//                Instantiate(weapon, targetPoint, Quaternion.identity);
                elapsedShootTime = 0;
            }
            explodePrepare = false;
        }
    }

    public void SetWeaponType(int type) {
        if (type == 1) {
            weapon = bullet;
            reloadTime = bullet.gameObject.GetComponent<Bullet> ().loadTime;
        } else if (type == 2) {
            weapon = canon;
            reloadTime = canon.gameObject.GetComponent<Canon> ().loadTime;
        } else if (type == 3) {
            weapon = missile;
            reloadTime = canon.gameObject.GetComponent<Missile> ().loadTime;
        }
    }

    void OnGUI() {
        if (weaponType == 1) {
            DrawTexture (canonBtn);
        } else if (weaponType == 2) {
            DrawTexture(missileBtn);
        } else if (weaponType == 3) {
            DrawTexture(bulletBtn);
        } 
    }


    public void DrawTexture(Texture2D texture) {
        float scale = 0.5f;
        Rect rect = new Rect (texture.width / 2 + 5, texture.height / 2 + 5, texture.width, texture.height);
        GUI.DrawTexture (rect, texture);
        
        if (GUI.Button (rect, "", new GUIStyle ())) {
            if (weaponType == 1) {
                weaponType = 2;
            } else if (weaponType == 2) {
                weaponType = 3;
            } else if (weaponType == 3) {
                weaponType = 1;
            }  
        }
    }
}
