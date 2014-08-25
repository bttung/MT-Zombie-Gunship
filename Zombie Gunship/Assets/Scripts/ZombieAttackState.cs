using UnityEngine;
using System.Collections;

public class ZombieAttackState : ZombieFSMState {

    protected Transform bulletSpawnPoint;
    private WeaponGun bullet;

    public ZombieAttackState() {
        stateID = ZombieFSMStateID.Atakking;
    }

    public override void Reason(Transform target, Transform human, Transform npc) {

        float dist = Vector3.Distance (npc.transform.position, human.transform.position);

        // Check the distance with human, When distance is near, transition to the chase state
        if (dist >= ATTACK_DIST_THRES && dist < LOST_DIST_THRES) {
//            Debug.Log ("Attack ---> Chase State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.SawHuman);

            // Stop Shooting
        }
        // Transition to patrol if human is too far
        else if (dist >= LOST_DIST_THRES) {
//            Debug.Log("Attack ---> Patrol State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.LostHuman);
//            Debug.Log("Dist " + dist);
            // Stop Shooting
        }

        // Check whether the zombie alive
        if (dead) {
//            Debug.Log ("Zombie Wanna Die");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.NoHealth);
        }
    }

    public override void Act(Transform target, Transform human, Transform npc) {
        // Attack Human
        npc.GetComponent<ZombieController> ().Attack ();
    }

}
