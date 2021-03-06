﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieFSMSystem {

    private List<ZombieFSMState> states;

    // The only way one can change the state of the FSM is by performing a transition.
    private ZombieFSMStateID currentStateID;
    public ZombieFSMStateID CurrentStateID {get {return currentStateID;}}

    private ZombieFSMState currentState;
    public ZombieFSMState CurrentState {get {return currentState;}}

    public ZombieFSMSystem() {
        states = new List<ZombieFSMState> ();
    }

    public void AddState(ZombieFSMState state) {
        // Check for Null Reference before Adding
        if (state == null) {
            Debug.LogError ("ZombieFSM Error: Null reference is not allowed");
        }

        // First State inserted also the initial state, the state the machine  is in when the simulation begins
        if (states.Count == 0) {
            states.Add(state);
            currentState = state;
            currentStateID = state.ID;
            return;
        }

        // Add the state to the List if it is not inside it
        foreach(ZombieFSMState s in states) {
            if (s.ID == state.ID) {
                Debug.LogError("ZombieFSM Error: Impossible to add state " + state.ID.ToString() + " because state has already been added");
                return;
            }
        }

        states.Add (state);
    }

    public void DeleteState(ZombieFSMStateID id) {
        // Check for None State before deleting
        if (id == ZombieFSMStateID.None) {
            Debug.LogError("ZombieFSM Error: None is not allowed for a real state");
            return;
        }

        // Search the list and delete the state if it's inside it
        foreach (ZombieFSMState state in states) {
            if (state.ID == id) {
                states.Remove(state);
                return;
            }
        }

        Debug.LogError ("ZombieFSM Error: Impossible to delete state " + id.ToString() + ". It was not on the list of states");
    } 

    public void PerformTransition(ZombieTransition trans) {
        // Check for None Transition before changing the current state
        if (trans == ZombieTransition.None) {
            Debug.LogError("ZombieFSM Error: None Transition is not allowed for a real transition");
            return;
        }

        // Check if the currentState has the transition passed as argument
        ZombieFSMStateID id = currentState.GetOutputState (trans);
        if (id == ZombieFSMStateID.None) {
            Debug.LogError ("ZombieFSM Error: State " + currentStateID.ToString() + " doesn't have a target state for transition " + trans.ToString());
            return;
        }
        
        // Update the currentState and currentStateID
        currentStateID = id;
        foreach (ZombieFSMState state in states) {
            if (state.ID == currentStateID) {
                // Do the post processing of the state before setting the new one
                currentState = state;
                // Reset the state to its desirec condition before it can reason or act
                break;
            }
        }
    }
    
}