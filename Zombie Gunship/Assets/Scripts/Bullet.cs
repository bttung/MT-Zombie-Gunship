using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    // Bullet is set as Laser and need Particle Effect
    public GameObject particleHit;
    public float speed = 100.0f;
    public float lifeTime =  1.0f;
    public int damage = 100;
    private GameObject display;
    Detonator detonator;

    // Use this for initialization
    void Start () {
        Destroy (gameObject, lifeTime);
        detonator = gameObject.GetComponent<Detonator> ();  
//        display = GameObject.FindGameObjectWithTag ("Display");  

        //  Because the camera is to high, the raycast generate some error in the y axiz, we need to replenish that error
//        gameObject.transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
        transform.GetComponent<SphereCollider> ().radius = 12f;
    }


    void OnTriggerEnter(Collider other) {
        string tagName = other.gameObject.tag;

        if (tagName == "Zombie") {
            other.gameObject.GetComponent<ZombieController>().TakeDamage(damage);
        } else if (tagName == "Human") {
            other.gameObject.GetComponent<HumanController>().TakeDamage(damage);
        }

        detonator.Explode();    
    
    }

//    void OnCollisionEnter(Collision col) {
//          Dont know why it dont enter this
//    }

}
