using UnityEngine;
using System.Collections;

public class ZombieWinState : ZombieFSMState{

    public ZombieWinState () {
        stateID = ZombieFSMStateID.Win;
    }

    public override void Reason(Transform target, Transform human, Transform npc) {
        // Zombie Win the game
    }

    public override void Act(Transform target, Transform human, Transform npc) {
        // Zombie Win the game
    }

}

