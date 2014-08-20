using UnityEngine;
using System.Collections;

public class ZombiePatrolState : ZombieFSMState {

    public ZombiePatrolState() {
        stateID = ZombieFSMStateID.Patrolling;

        curRotSpeed = 1.0f;
        curSpeed = 100.0f;
    }

    public override void Reason(Transform human, Transform npc) {
        // Check the distance with human
        // When distance is near, transition to the chase state
        if (Vector3.Distance (npc.position, human.position) <= 300.0f) {
            Debug.Log ("Switch to Chase State");

//            npc.GetComponent<ZombieController>().SetTransition(Transition.SawHuman);
        }
    }

    public override void Act(Transform human, Transform npc) {
        // If the target is reached
        if (Vector3.Distance (npc.position, desPos) <= 100.0f) {
            Debug.Log ("Reachec the desitnation");

            // Do something here...
        }

        // Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation (desPos - npc.position);

        npc.rotation = Quaternion.Slerp (npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        // Go Forward
        npc.Translate (Vector3.forward * Time.deltaTime * curSpeed);
    }
}
