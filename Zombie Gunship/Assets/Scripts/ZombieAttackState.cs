using UnityEngine;
using System.Collections;

public class ZombieAttackState : ZombieFSMState {

    protected Transform bulletSpawnPoint;
    private WeaponGun bullet;

    public override void Reason(GameObject human, GameObject npc) {
//        // Check if the distance with human
//        float dist = Vector3.Distance (npc.position, human.position);
//        if (dist >= 50 && dist < 100.0f) {
//            Debug.Log ("Switch to Chase State");
//            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.SawHuman);
//
//            // Stop Shooting
//        }
//        // Transition to patrol if human is too far
//        else if (dist >= 100.0f) {
//            Debug.Log("Switch to Patrol State");
//            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.LostHuman);
//
//            // Stop Shooting
//        }
    }

    public override void Act(GameObject human, GameObject npc) {
//        // Set the target position as human position
//        desPos = human.position + new Vector3 (0.0f, 1.0f, 0.0f);
//
//        elapsedTime = 0;
//        shootRate = 3.0f;
//
//        // Start Shooting
//        //Debug.Log ("Shoot");
    }

}
