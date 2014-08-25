using UnityEngine;
using System.Collections;

public class ZombieDeadState : ZombieFSMState {

    public ZombieDeadState() {
        stateID = ZombieFSMStateID.Dead;
    }

    public override void Reason(Transform target, Transform human, Transform npc) {
        // You Died!
    }
    
    public override void Act(Transform target, Transform human, Transform npc) {
        // Died
        if (!dead) {
            dead = true;
            health = 0;
        }

        // Make the NPC explode then destroy it
        npc.GetComponent<ZombieController> ().Explode ();
    }
}
