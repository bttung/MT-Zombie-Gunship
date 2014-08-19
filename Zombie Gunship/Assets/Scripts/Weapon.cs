using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public GameObject bullet;
    public float ratePerSecond;
    public bool shoot;

	// Use this for initialization
	void Start () {
        bool = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot() {
        shoot = true;

        StartCoroutine ("ShootBullets");
    }

    public void StopShoot() {
        if (shoot) {
            shoot = false;
        }

        StopCoroutine ("ShootBullets");
    }

    IEnumerator ShootBullets() {
        SpawnBullet ();
        yield return new WaitForSeconds(1.0f / ratePerSecond);
        StartCoroutine ("ShootBullets");
    }

    private void SpawnBullet() {
        Vector3 spawnPos;
        Vector3 spawnRot;

        GameObject objBullet = (GameObject)Instantiate (Bullet, spawnPos, spawnRot);
    }
}
