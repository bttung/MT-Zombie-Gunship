using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanFSMSystem {

    private List<HumanFSMState> states;

    // The only way one can change the state of the FSM is by performing a transition.
    private HumanFSMStateID currentStateID;
    public HumanFSMStateID CurrentStateID {get {return currentStateID;}}
    
    private HumanFSMState currentState;
    public HumanFSMState CurrentState {get {return currentState;}}

    public HumanFSMSystem() {
        states = new List<HumanFSMState> ();
    }

    public void AddState(HumanFSMState state) {
        // Check for Null Reference before Adding
        if (state == null) {
            Debug.LogError ("HumanFSM Error: Null reference is not allowed");
        }

        // First State inserted also the initial state, the state the machine  is in when the simulation begins
        if (states.Count == 0) {
            states.Add(state);
            currentState = state;
            currentStateID = state.ID;
            return;
        }
        
        // Add the state to the List if it is not inside it
        foreach(HumanFSMState s in states) {
            if (s.ID == state.ID) {
                Debug.LogError("HumanFSM Error: Impossible to add state " + state.ID.ToString() + " because state has already been added");
                return;
            }
        }
        
        states.Add (state);
    }

    public void DeleteState(HumanFSMStateID id) {
        // Check for None State before deleting
        if (id == HumanFSMStateID.None) {
            Debug.LogError("HumanFSM Error: None is not allowed for a real state");
            return;
        }
        
        // Search the list and delete the state if it's inside it
        foreach (HumanFSMState state in states) {
            if (state.ID == id) {
                states.Remove(state);
                return;
            }
        }
        
        Debug.LogError ("HumanFSM Error: Impossible to delete state " + id.ToString() + ". It was not on the list of states");
    }

    public void PerformTransition(HumanTransition trans) {
        // Check for None Transition before changing the current state
        if (trans == HumanTransition.None) {
            Debug.LogError("HumanFSM Error: None Transition is not allowed for a real transition");
            return;
        }
        
        // Check if the currentState has the transition passed as argument
        HumanFSMStateID id = currentState.GetOutputState (trans);
        if (id == HumanFSMStateID.None) {
            Debug.LogError ("HumanFSM Error: State " + currentStateID.ToString() + " doesn't have a target state for transition " + trans.ToString());
            return;
        }
        
        // Update the currentState and currentStateID
        currentStateID = id;
        foreach (HumanFSMState state in states) {
            if (state.ID == currentStateID) {
                // Do the post processing of the state before setting the new one
                currentState = state;
                // Reset the state to its desirec condition before it can reason or act
                break;
            }
        }
    }

}
