﻿using UnityEngine;
using System.Collections;

public class HumanController : MonoBehaviour {

    public Transform[] path;
    private HumanFSMSystem fsm;

    public void SetTransition(HumanTransition trans) {fsm.PerformTransition(trans);}

    public void Start() {
        ConstructFSM ();
    }
    
    public void FixedUpdate() {
        fsm.CurrentState.Reason (gameObject);
        fsm.CurrentState.Act (gameObject);
    }
    
    // The NPC has 4 states: Patrol, Chasing, Attack, Dead
    private void ConstructFSM() {
        HumanFleeState flee = new HumanFleeState (path);
        flee.AddTransition (HumanTransition.ReachShelter, HumanFSMStateID.Sheltering);
        flee.AddTransition (HumanTransition.NoHealth, HumanFSMStateID.Dead);
        
        HumanShelterState shelter = new HumanShelterState ();
        shelter.AddTransition (HumanTransition.ReachShelter, HumanFSMStateID.Sheltering);
        
        HumanDeadState dead = new HumanDeadState ();
        dead.AddTransition (HumanTransition.NoHealth, HumanFSMStateID.Dead);
        
        fsm = new HumanFSMSystem ();
        fsm.AddState (flee);
        fsm.AddState (shelter);
        fsm.AddState (dead);
    }
    
    public void Explode() {
        float rndX = Random.Range (10.0f, 30.0f);
        float rndZ = Random.Range (10.0f, 30.0f);
        for (int i = 0; i < 3; i++) {
            rigidbody.AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
            rigidbody.velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
        }
        
        Destroy (gameObject, 1.5f);
    }


    public void TakeDamage(int damage) {
        if (fsm.CurrentStateID == HumanFSMStateID.Dead) {
            return;
        }

        fsm.CurrentState.TakeDamage (damage);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Shelter") {
            SetTransition(HumanTransition.ReachShelter);
        }
    }
}
