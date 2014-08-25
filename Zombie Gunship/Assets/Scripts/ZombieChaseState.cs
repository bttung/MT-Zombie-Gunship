using UnityEngine;
using System.Collections;

public class ZombieChaseState : ZombieFSMState  {

    public ZombieChaseState() {
        stateID = ZombieFSMStateID.Chasing;
    }

    // Check the new reason to change state
    public override void Reason(Transform target, Transform human, Transform npc) {

        float dist = Vector3.Distance (npc.transform.position, human.transform.position);

        // Check the distance with human, when the distance is near, transition to attack state
        if (dist <= ATTACK_DIST_THRES) {
//            Debug.Log("Chase ---> Attack State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.ReachHuman);
        }

        // Go back to patrol if it become too far
        if (dist >= SAW_DIST_THRES) {
//            Debug.Log ("Chase ---> Patrol State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.LostHuman);
        }

        // Check whether the zombie alive
        if (dead) {
//            Debug.Log ("Zombie Wanna Die");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.NoHealth);
        }
    }
    
    public override void Act(Transform target, Transform human, Transform npc) {
        // Navigation use NavMeshAgent
        npc.GetComponent<ZombieController> ().agent.SetDestination (human.position);
    }
}
