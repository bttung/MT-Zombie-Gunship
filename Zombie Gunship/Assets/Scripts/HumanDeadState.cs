using UnityEngine;
using System.Collections;

public class HumanDeadState : HumanFSMState {

    public HumanDeadState() {
        stateID = HumanFSMStateID.Dead;
    }

    public override void Reason(Transform target, Transform npc) {
        // You Died!
    }

    public override void Act(Transform target, Transform npc) {
        // Died
        if (!dead) {
            dead = true;
            health = 0;
        }

        // Make the NPC explode then destroy it
        Debug.Log ("U Human wanna die ...");
        npc.GetComponent<HumanController> ().Explode ();
    }
}
