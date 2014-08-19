using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    // Bullet is set as Laser and need Particle Effect
    public GameObject particleHit;
    public float speed = 100.0f;
    public float lifeTime = 3.0f;
    public int damage = 50;

	// Use this for initialization
	void Start () {
        Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
//        transform.Translate (new Vector3 (0, 0, speed * Time.deltaTime));
        transform.position += transform.forward * speed * Time.deltaTime;
	}

    void OnCollisionEnter(Collision collision) {
        Vector3 contactPoint = collision.contacts [0].point;

        Instantiate (particleHit, contactPoint, Quaternion.identity);
        Destroy (gameObject);
    }
}
