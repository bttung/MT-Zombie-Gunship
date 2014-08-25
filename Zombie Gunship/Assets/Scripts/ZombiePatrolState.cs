using UnityEngine;
using System.Collections;

public class ZombiePatrolState : ZombieFSMState {

    private int currentWayPoint;

    public ZombiePatrolState() {
        currentWayPoint = 0;
        stateID = ZombieFSMStateID.Patrolling;
    }

    public override void Reason(Transform target, Transform human, Transform npc) {
        // Check the distance with human, When distance is near, transition to the chase state
        float dist = Vector3.Distance (npc.transform.position, human.transform.position);
        if (dist <= SAW_DIST_THRES) {
//            Debug.Log ("Patrol ---> Chase State");
//            Debug.Log("Dist " + dist);
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.SawHuman);
        }

        // Check whether the zombie alive
        if (dead) {
//            Debug.Log ("Zombie Wanna Die");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.NoHealth);
        }
    }

    public override void Act(Transform target, Transform human, Transform npc) {
        // Navigation use NavMeshAgent
        npc.GetComponent<ZombieController> ().agent.SetDestination (target.position);
    }
}
