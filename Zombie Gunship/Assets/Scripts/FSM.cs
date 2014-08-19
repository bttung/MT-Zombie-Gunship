using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {

    // Player Transform
    protected Transform playerTransform;

    // Next Destination position of Zombie
    protected Vector3 destPos;

    // List of points for patrolling
    protected GameObject[] pointList;

    // Bullet shooting rate
    protected float shootRate;
    protected float elapsedTime;

    // Weapon
    public Transform weapon { get; set;}
    public Transform bulletSpawnPoint { get; set;}

    protected virtual void Initialize() {}
    protected virtual void FSMUpdate() {}
    protected virtual void FSMFixedUpdate() {}


	// Use this for initialization
	void Start () {
        Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
        FSMUpdate ();
	}

    void FixedUpdate() {
        FSMFixedUpdate ();
    }
}
