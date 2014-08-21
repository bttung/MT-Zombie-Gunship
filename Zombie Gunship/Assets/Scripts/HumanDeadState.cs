using UnityEngine;
using System.Collections;

public class HumanDeadState : HumanFSMState {

    public HumanDeadState() {
        stateID = HumanFSMStateID.Dead;
    }

    public override void Reason(GameObject npc) {
        // You Died!
    }

    public override void Act(GameObject npc) {
        // Died
        if (!dead) {
            dead = true;
            health = 0;
        }

        // Make the NPC explode then destroy it
        npc.GetComponent<HumanController> ().Explode ();
    }
}
