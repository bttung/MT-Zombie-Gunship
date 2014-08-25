using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum HumanTransition {
    None = 0,
    RunAway,
    ReachShelter,
    NoHealth,
}

public enum HumanFSMStateID {
    None = 0,
    Fleeing,
    Sheltering,
    Dead,
}

public abstract class HumanFSMState {

    protected Dictionary<HumanTransition, HumanFSMStateID> map = new Dictionary<HumanTransition, HumanFSMStateID> ();
    protected HumanFSMStateID stateID;
    public HumanFSMStateID ID {get {return stateID;} }

    // whether Human died or not
    protected bool dead = false;
    protected int health = 100;

    public void AddTransition(HumanTransition trans, HumanFSMStateID id) {
        // Check if anyone of the args is invalid
        if (trans == HumanTransition.None) {
            Debug.LogError ("HumanFSMState Error: None is not allowed for a real Transition");
            return;
        }
        
        if (id == HumanFSMStateID.None) {
            Debug.LogError ("HumanFSMState Error: None is not allowed for a real ID");
            return;
        }
        
        // Since this is a Deterministic FSM, check fi the current transition was already inside the map
        if (map.ContainsKey (trans)) {
            Debug.LogError("HumanFSMState Error: State " + stateID.ToString() + "already has transition" + trans.ToString() + " Impossible to assign to another state");
            return;
        }
        
        map.Add (trans, id);
    }

    public void DeleteTransition(HumanTransition trans) {
        // Check for None Transition
        if (trans == HumanTransition.None) {
            Debug.LogError("HumanFSMState Error: None transition is not allowed");
            return;
        }
        
        // Check if the pair is inside the map before deleting
        if (map.ContainsKey (trans)) {
            map.Remove(trans);
            return;
        }
        
        Debug.LogError ("HumanFSMState Error: Transition " + trans.ToString() + " passed to " + stateID.ToString() + "was not on the state's transition list");
    }

    public HumanFSMStateID GetOutputState(HumanTransition trans) {
        // Check if the map has this transition
        if (map.ContainsKey (trans)) {
            return map[trans];
        }
        return HumanFSMStateID.None;
    }

    public void TakeDamage(int damage) {
        if (dead) {
            return;
        }

        health -= damage;
        if (health <= 0) {
            dead = true;
            health = 0;
        }
    }
    
    public abstract void Reason(Transform target, Transform npc);
    public abstract void Act(Transform target, Transform npc);

}
