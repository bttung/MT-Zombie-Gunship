using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    // Bullet is set as Laser and need Particle Effect
    public GameObject particleHit;
    public float speed = 100.0f;
    public float lifeTime = 0.1f;
    public int damage = 100;
    private GameObject display;
    Detonator detonator;

    // Use this for initialization
    void Start () {
        Destroy (gameObject, lifeTime);
        display = GameObject.FindGameObjectWithTag ("Display");
        detonator = gameObject.GetComponent<Detonator> ();

        if (detonator == null) {
            Debug.LogError("Bullet not found Detonator");
        }
    }
    
    // Update is called once per frame
    void Update () {
        //        transform.Translate (new Vector3 (0, 0, speed * Time.deltaTime));
        //        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    void OnCollisionEnter(Collision col) {
//        Vector3 contactPoint = collision.contacts [0].point;        
//        Instantiate (particleHit, contactPoint, Quaternion.identity);
//        Destroy (gameObject);

        string tagName = col.gameObject.tag;
        if (tagName == "Zombie") {
            col.gameObject.GetComponent<ZombieController>().TakeDamage(damage);
        } else if (tagName == "Human") {
            col.gameObject.GetComponent<HumanController>().TakeDamage(damage);
        }

        display.gameObject.GetComponent<StatusDisplay> ().RefreshMessage ("Bullet Killed someone");
//        detonator.gameObject.transform.position = targetPoint;

        Debug.Log ("Bullet make detonator Explode");
        detonator.Explode();
    }
}
