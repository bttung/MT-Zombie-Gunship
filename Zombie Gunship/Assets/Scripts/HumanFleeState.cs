using UnityEngine;
using System.Collections;

public class HumanFleeState : HumanFSMState {

    private int currentWayPoint;
    private Transform[] wayPoints;

    public HumanFleeState(Transform[] wp) {
        wayPoints = wp;
        currentWayPoint = 0;
        stateID = HumanFSMStateID.Fleeing;
    }

    public override void Reason(GameObject npc) {
        // Check whether the zombie alive
        if (dead) {
            Debug.Log ("Human Wanna Die");
            npc.GetComponent<HumanController>().SetTransition(HumanTransition.NoHealth);
        }
    }

    public override void Act(GameObject npc) {
        // Follow the path of waypoints, Find the direction of the current way point
        Vector3 vel = npc.rigidbody.velocity;
        Vector3 moveDir = wayPoints[currentWayPoint].position - npc.transform.position;
        
        if (moveDir.magnitude < 5.0f) {
            Debug.Log ("Reached the desitnation");
            currentWayPoint++;
            if (currentWayPoint >= wayPoints.Length) {
                currentWayPoint = 0;
                
                // If human entered the shelter ...
                
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
