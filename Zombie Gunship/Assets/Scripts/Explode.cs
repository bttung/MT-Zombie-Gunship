using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

    Detonator detonator;

	// Use this for initialization
	void Start () {
        detonator = gameObject.GetComponent<Detonator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.anyKey) {
            detonator.Explode();
        }
	}
}
