using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    private float speed = 100.0f;
    private float lifeTime =  1.0f;
    private int damage = 100;
    private float radius = 12f;
    public float loadTime = 1.0f;

    Detonator detonator;
    
    // Use this for initialization
    void Start () {
        Init ();
    }

    protected void Init() {
//        Destroy (gameObject, lifeTime);
        detonator = gameObject.GetComponent<Detonator> ();  
        //        display = GameObject.FindGameObjectWithTag ("Display");  
        
        //  Because the camera is to high, the raycast generate some error in the y axiz, we need to replenish that error
        //        gameObject.transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
        transform.GetComponent<SphereCollider> ().radius = radius;
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

    public void UpdateDamageRadius(float radius) {
        this.radius = radius;
    }

    //    void OnCollisionEnter(Collision col) {
    //          Dont know why it dont enter this
    //    }
    
}
