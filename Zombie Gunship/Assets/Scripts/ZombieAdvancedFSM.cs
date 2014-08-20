using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ZombieTransition {
    None = 0,
    SawHuman,
    ReachHuman,
    LostHuman,
    NoHealth,
}

public enum ZombieFSMStateID {
    None = 0,
    Patrolling,
    Chasing,
    Atakking,
    Dead,
}

public class ZombieAdvancedFSM : MonoBehaviour {

    private List<ZombieFSMState> fsmStates;
    private ZombieFSMState curFSMState;
    private ZombieFSMStateID curFSMStateID;

    // whether the Zombie is destroyed or not
    private bool dead;
    private int health;
 
    protected Transform humanTransform;

    // Method that Implements Class Should Override.
    protected virtual void Initialize() {}
    protected virtual void FSMUpdate() {}
    protected virtual void FSMFixedUpdate() {}

    public ZombieFSMState GetFSMState() {
        return curFSMState;
    }

    public ZombieFSMStateID GetFSMStateID() {
        return curFSMStateID;
    }

    protected void AddFSMState(ZombieFSMState state) {
        fsmStates.Add (state);
    }

    protected void DeleteFSMState(ZombieFSMState state) {
        fsmStates.Remove (state);
    }
    
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
    
    public void SetTransition(ZombieTransition transition) {
        PerformTransition (transition);
    }
    
    public void PerformTransition(ZombieTransition trans) {
        // Check if the currentState has the transition passed as a argument
        // ZombieFSMStateID id = curFSMState.GetOutPutState (trans);
        ZombieFSMStateID id = curFSMState.GetOutputState (trans);

        if (id == ZombieFSMStateID.None) {
            Debug.Log ("FSM Error: Current State does not have a target state for transition");
            return;
        }

        // Update the current State and current State ID

        // There is something wrong here
        curFSMStateID = id;
        foreach (ZombieFSMState state in fsmStates) {
            if (state.stateID == curFSMStateID) {
//            if (state.GetStateID() == curFSMStateID) {
                curFSMState = state;
                break;
            }
        }

    }

    protected void Explode() {
        float rndX = Random.Range (10.0f, 30.0f);
        float rndZ = Random.Range (10.0f, 30.0f);
        for (int i = 0; i < 3; i++) {
            rigidbody.AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
            rigidbody.velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
        }
        
        Destroy (gameObject, 1.5f);
    }

    // Taking Damage when Hit with Missile or Bullet.
    void OnCollisionEnter(Collision collision) {
        if (dead) {
            return;
        }

        if (collision.gameObject.tag == "Bullet") {
            Debug.Log ("Hit with Bullet");
            health -= 30;
        } else if (collision.gameObject.tag == "Missile") {
            Debug.Log("Hit with Missile");
            health -= 50;
        }

        if (health <= 0) {
            dead = true;
            Explode();
            Destroy (gameObject, 4.0f);
        }
    }
}