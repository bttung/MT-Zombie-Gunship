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

public abstract class ZombieFSMState {

    protected Dictionary<ZombieTransition, ZombieFSMStateID> map = new Dictionary<ZombieTransition, ZombieFSMStateID>();
    public ZombieFSMStateID stateID;
    public ZombieFSMStateID ID {get {return stateID;} }

    // whether the Zombie is destroyed or not
    protected bool dead = false; 
    protected int health = 100;

//    protected float curRotSpeed;
//    protected float curSpeed;
//    protected Vector3 desPos;
//
//    // Bullet
//    public GameObject bullet;
//    protected float shootRate;
//    protected float elapsedTime;

    public void AddTransition(ZombieTransition trans, ZombieFSMStateID id) {
        // Check if anyone of the args is invalid
        if (trans == ZombieTransition.None) {
            Debug.LogError ("FSMState Error: None is not allowed for a real Transition");
            return;
        }

        if (id == ZombieFSMStateID.None) {
            Debug.LogError ("FSMState Error: None is not allowed for a real ID");
            return;
        }

        // Since this is a Deterministic FSM, check fi the current transition was already inside the map
        if (map.ContainsKey (trans)) {
            Debug.LogError("FSMState Error: State " + stateID.ToString() + "already has transition" + trans.ToString() + " Impossible to assign to another state");
            return;
        }

        map.Add (trans, id);
    }
    
    public void DeleteTransition(ZombieTransition trans) {
        // Check for None Transition
        if (trans == ZombieTransition.None) {
            Debug.LogError("FSMState Error: None transition is not allowed");
            return;
        }

        // Check if the pair is inside the map before deleting
        if (map.ContainsKey (trans)) {
            map.Remove(trans);
            return;
        }

        Debug.LogError ("FSMState Error: Transition " + trans.ToString() + " passed to " + stateID.ToString() + "was not on the state's transition list");
    }


    public ZombieFSMStateID GetOutputState(ZombieTransition trans) {
        // Check if the map has this transition
        if (map.ContainsKey (trans)) {
            return map[trans];
        }
        return ZombieFSMStateID.None;
    }

    public void TakeDamage(int damage) {
        health -= damage;
    }

    public void Die() {
        dead = true;
    }

    public abstract void Reason(GameObject human, GameObject npc);
    public abstract void Act(GameObject human, GameObject npc);

}
