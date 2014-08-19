using UnityEngine;
using System.Collections;

public class WeaponBullet : MonoBehaviour {

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
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;
        
        GameObject objBullet = (GameObject)Instantiate (bullet, spawnPos, spawnRot);
    }
}
