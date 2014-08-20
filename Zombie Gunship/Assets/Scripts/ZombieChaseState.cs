using UnityEngine;
using System.Collections;

public class ZombieChaseState : ZombieFSMState  {

    // Check the new reason to change state
    public override void Reason(Transform human, Transform npc) {
        // Set the target position as human position
        desPos = human.position;

        // Check the distance with human, when the distance is near, transition to attack state
        float dist = Vector3.Distance (npc.position, desPos);
        if (dist <= 60.0f) {
            Debug.Log("Switch to Attack State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.ReachHuman);
        }

        // Go back to patrol if it become too far
        if (dist >= 110.0f) {
            Debug.Log ("Switch to Patrol State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.LostHuman);
        }
    }
    
    public override void Act(Transform human, Transform npc) {
        // Rotate to the target point
        desPos = human.position;

        // Head to the destination .... 
    }
}
