using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieFSMSystem {

    private List<ZombieFSMState> states;

    // The only way one can change the state of the FSM is by performing a transition.
    private ZombieFSMStateID currentStateID;
    public ZombieFSMStateID CurrentStateID {get {return currentStateID;}}

    private ZombieFSMState currentState;
    public ZombieFSMState CurrentState {get {return currentState;}}

    public void AddState(ZombieFSMState state) {
        // Check for Null Reference before Adding
        if (state == null) {
            Debug.LogError ("FSM Error: Null reference is not allowed");
        }

        if (states.Count == 0) {
            states.Add(state);
            currentState = state;
            currentStateID = state.ID;
            return;
        }

        // Add the state to the List if it is not inside it
        foreach(ZombieFSMState s in states) {
            if (s.ID == state.ID) {
                Debug.LogError("FSM Error: Impossible to add state " + state.ID.ToString() + " because state has already been added");
                return;
            }
        }

        states.Add (state);
    }

    public void DeleteState(ZombieFSMStateID id) {
        // Check for None State before deleting
        if (id == ZombieFSMStateID.None) {
            Debug.LogError("FSM Error: None is not allowed for a real state");
            return;
        }

        // Search the list and delete the state if it's inside it
        foreach (ZombieFSMState state in states) {
            if (state.ID == id) {
                states.Remove(state);
                return;
            }
        }

        Debug.LogError ("FSM Error: Impossible to delete state " + id.ToString() + ". It was not on the list of states");
    }

//    // whether the Zombie is destroyed or not
//    private bool dead;
//    private int health;

    public void PerformTransition(ZombieTransition trans) {
        // Check for None Transition before changing the current state
        if (trans == ZombieTransition.None) {
            Debug.LogError("FSM Error: None Transition is not allowed for a real transition");
            return;
        }

        // Check if the currentState has the transition passed as argument
        ZombieFSMStateID id = currentState.GetOutputState (trans);
        if (id == ZombieFSMStateID.None) {
            Debug.LogError ("FSM Error: State " + currentStateID.ToString() + " doesn't have a target state for transition " + trans.ToString());
            return;
        }
        
        // Update the currentState and currentStateID
        currentStateID = id;
        foreach (ZombieFSMState state in states) {
            if (state.ID == currentStateID) {
                currentState = state;
                break;
            }
        }
        
    }
    
//    protected void Explode() {
//        float rndX = Random.Range (10.0f, 30.0f);
//        float rndZ = Random.Range (10.0f, 30.0f);
//        for (int i = 0; i < 3; i++) {
//            rigidbody.AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
//            rigidbody.velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
//        }
//        
//        Destroy (gameObject, 1.5f);
//    }
//    
//    // Taking Damage when Hit with Missile or Bullet.
//    void OnCollisionEnter(Collision collision) {
//        if (dead) {
//            return;
//        }
//        
//        if (collision.gameObject.tag == "Bullet") {
//            Debug.Log ("Hit with Bullet");
//            health -= 30;
//        } else if (collision.gameObject.tag == "Missile") {
//            Debug.Log("Hit with Missile");
//            health -= 50;
//        }
//        
//        if (health <= 0) {
//            dead = true;
//            Explode();
//            Destroy (gameObject, 4.0f);
//        }
//    }

}