using UnityEngine;
using System.Collections;

public class LivingCreature : MonoBehaviour {

    public bool dead;
    public float heath;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision) {
         
        if (dead) return;

        if (collider.gameObject.tag == "Bullet") {
            // take damage
        } else if (collider.gameObject.tag == "Canon") {
            // take damage
        } else if (collider.gameObject.tag == "Missile") {
            // take damage
        }

        if (heath <= 0) {
            dead = true;
            Expode();
            Destroy(gameObject, 4.0f);
        }
    }

    void Expode() {

    }
}
