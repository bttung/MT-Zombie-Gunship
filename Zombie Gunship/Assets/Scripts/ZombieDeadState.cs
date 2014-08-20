using UnityEngine;
using System.Collections;

public class ZombieDeadState : ZombieFSMState {

    public override void Reason(GameObject human, GameObject npc) {
        // You Died!
    }
    
    public override void Act(GameObject human, GameObject npc) {
        // Died, Cannot Do Nothing
        if (!dead) {
            dead = true;
            health = 0;
        }
    }
}
