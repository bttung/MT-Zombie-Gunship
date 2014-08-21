using UnityEngine;
using System.Collections;

public class ZombieWinState : ZombieFSMState{

    public ZombieWinState () {
        stateID = ZombieFSMStateID.Win;
    }

    public override void Reason(GameObject human, GameObject npc) {
        // Zombie Win the game
    }

    public override void Act(GameObject human, GameObject npc) {
        // Zombie Win the game
    }

}

