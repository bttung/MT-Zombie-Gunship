using UnityEngine;
using System.Collections;

public class ZombieChaseState : ZombieFSMState  {

    public ZombieChaseState() {
        stateID = ZombieFSMStateID.Chasing;
    }

    // Check the new reason to change state
    public override void Reason(GameObject human, GameObject npc) {
        // Check the distance with human, when the distance is near, transition to attack state
        if (Vector3.Distance (npc.transform.position, human.transform.position) <= 60.0f) {
            Debug.Log("Switch to Attack State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.ReachHuman);
        }

        // Go back to patrol if it become too far
        if (Vector3.Distance (npc.transform.position, human.transform.position) >= 110.0f) {
            Debug.Log ("Switch to Patrol State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.LostHuman);
        }

        // Check whether the zombie alive
        if (dead || health <= 0) {
            Debug.Log ("Zombie Wanna Die");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.NoHealth);
        }
    }
    
    public override void Act(GameObject human, GameObject npc) {
        // Follow the path of waypoints, Find the direction of human
        Vector3 vel = npc.rigidbody.velocity;
        Vector3 moveDir = human.transform.position - npc.transform.position;

        // Rotate toward the waypoint
        npc.transform.rotation = Quaternion.Slerp (npc.transform.rotation, Quaternion.LookRotation (moveDir), 5 * Time.deltaTime);
        npc.transform.eulerAngles = new Vector3 (0, npc.transform.eulerAngles.y, 0);

        vel = moveDir.normalized * 10;

        // Apply the Velocity
        npc.rigidbody.velocity = vel;
    }
}
