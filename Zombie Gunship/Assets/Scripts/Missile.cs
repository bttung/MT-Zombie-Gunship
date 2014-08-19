using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    public GameObject particleHit;
    public float speed = 20.0f;
    private Transform target;

    public void Initialise(bool hasTarget, Transform target = null1) {
        if (hasTarget) {
            this.tag = target;
            Destroy (gameObject, 4.0f);
        } else {
            Destroy(gameObject, 2.0f);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (target != null) {
            Vector3 newTarPos = target.position + new Vector3(0.0f, 1.0f, 0.0f);

            // Rotate toward the target
            Vector3 tarDir = newTarPos - transform.position;
            Quaternion tarRot = Quaternion.LookRotation(tarDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, 3.0f * Time.deltaTime);
        }
	}

    void OnCollisionEnter(Collision collision) {
        Vector3 contactPoint = collision.contacts [0].point;
        Instantiate (particleHit, contactPoint, Quaternion.identity);
        Destroy (gameObject);
    }
}
