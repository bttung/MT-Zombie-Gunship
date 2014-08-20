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

public class ZombieAdvancedFSM : MonoBehaviour{

    private List<ZombieFSMState> fsmStates;
    protected virtual void Initialize() {}
    protected virtual void FSMUpdate() {}
    protected virtual void FSMFixedUpdate() {}

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
}