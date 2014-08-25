using UnityEngine;
using System.Collections;

public class WeaponGun : MonoBehaviour {

    public GameObject bullet;
    public float ratePerSecond;
    public bool shoot;
    
    // Use this for initialization
    void Start () {
        shoot = false;
    }
    
    // Update is called once per frame
    void Update () {
        
    }
    
    public void Shoot() {
        shoot = true;

        // Do animation here ...

        StartCoroutine ("ShootBullets");
    }
    
    public void StopShoot() {
        if (shoot) {
            shoot = false;

            // Stop animation here ...
        }
        
        StopCoroutine ("ShootBullets");
    }
    
    IEnumerator ShootBullets() {
        SpawnBullet ();
        yield return new WaitForSeconds(1.0f / ratePerSecond);
        StartCoroutine ("ShootBullets");
    }
    
    private void SpawnBullet() {
        // Set the position to spawn the bullet
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        // Create a new bullet
        GameObject objBullet = (GameObject)Instantiate (bullet, spawnPos, spawnRot);
    }
}
