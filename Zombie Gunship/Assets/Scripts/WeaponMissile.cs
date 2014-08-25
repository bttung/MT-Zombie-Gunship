using UnityEngine;
using System.Collections;

public class WeaponMissile : MonoBehaviour {

    public GameObject missile;
    public Transform spawnPoint;
    private bool shoot;
    private bool hasTarget;
    private Transform target;

	// Use this for initialization
	void Start () {
        shoot = false;
        hasTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot() {
        // Check whether target exist or not
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hitInfo;

        // RayCast only to zombie with layer number is 9
        int layerMask = 1 << 9;

        if (Physics.Raycast (ray, out hitInfo, 1000.0f, layerMask)) {
            hasTarget = true;
            target = hitInfo.transform;
        } else {
            hasTarget = false;
        }

        shoot = true;
        StartCoroutine("ShootMissiles");
    }

    public void StopShoot() {
        // Stop the shooting animation
        if (shoot) {
            shoot = false;
        }
        StopCoroutine ("ShootMissiles");
    }

    private IEnumerator ShootMissiles() {
        SpawnMissiles ();
        yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
        StartCoroutine ("ShootMissiles");
    }

    private void SpawnMissiles() {
        // Create a new Missile
        GameObject objMissile = (GameObject)Instantiate (missile, spawnPoint.position, spawnPoint.rotation);
        objMissile.GetComponent<MissileController> ().Initialise (hasTarget, target);
    }

}
