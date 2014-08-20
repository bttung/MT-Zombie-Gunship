using UnityEngine;
using System.Collections;

public class ZombiePatrolState : ZombieFSMState {

    private int currentWayPoint;
    private Transform[] wayPoints;

    public ZombiePatrolState(Transform[] wp) {
        wayPoints = wp;
        currentWayPoint = 0;
        stateID = ZombieFSMStateID.Patrolling;
    }

    public override void Reason(GameObject human, GameObject npc) {
        // Check the distance with human, When distance is near, transition to the chase state
        if (Vector3.Distance (npc.transform.position, human.transform.position) <= 300.0f) {
            Debug.Log ("Switch to Chase State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.SawHuman);
        }
    }

    public override void Act(GameObject human, GameObject npc) {
        // Follow the path of waypoints, Find the direction of the current way point
        Vector3 vel = npc.rigidbody.velocity;
        Vector3 moveDir = wayPoints[currentWayPoint].position - npc.transform.position;

        if (moveDir.magnitude < 5.0f) {
            Debug.Log ("Reached the desitnation");
            currentWayPoint++;
            if (currentWayPoint >= wayPoints.Length) {
                currentWayPoint = 0;

                // If zombie entered the shelter, GameOver

            }
        } else {
            vel = moveDir.normalized * 10;

            // Rotate towards the waypoint
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(moveDir), 5 * Time.deltaTime);
            npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);           
        }

        // Apply the Velocity
        npc.rigidbody.velocity = vel;
    }
}
