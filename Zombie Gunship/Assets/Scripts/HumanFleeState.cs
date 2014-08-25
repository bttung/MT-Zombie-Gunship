using UnityEngine;
using System.Collections;

public class HumanFleeState : HumanFSMState {

    public HumanFleeState() {
        stateID = HumanFSMStateID.Fleeing;
    }

    public override void Reason(Transform target, Transform npc) {
        // Check whether the zombie alive
        if (dead) {
//            Debug.Log ("Human Wanna Die");
            npc.GetComponent<HumanController>().SetTransition(HumanTransition.NoHealth);
        }
    }

    public override void Act(Transform target, Transform npc) {
        // Navigation use NavMeshAgent
        npc.GetComponent<HumanController> ().agent.SetDestination(target.position);
    }
}
