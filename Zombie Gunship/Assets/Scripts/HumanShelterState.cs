using UnityEngine;
using System.Collections;

public class HumanShelterState : HumanFSMState {

    public HumanShelterState() {
        stateID = HumanFSMStateID.Sheltering;
    }

    public override void Reason (Transform target, Transform npc) {
        // You are safe here, no enemy can attack you
    }

    public override void Act(Transform target, Transform npc) {
        // You are safe here, no enemy can attack you
    }
}
