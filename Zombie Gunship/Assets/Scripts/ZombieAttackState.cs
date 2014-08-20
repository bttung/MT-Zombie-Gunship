using UnityEngine;
using System.Collections;

public class ZombieAttackState : ZombieFSMState {

    protected Transform bulletSpawnPoint;
    private WeaponGun bullet;

    public ZombieAttackState() {
        stateID = ZombieFSMStateID.Atakking;
    }

    public override void Reason(GameObject human, GameObject npc) {

        // Check the distance with human, When distance is near, transition to the chase state
        float dist = Vector3.Distance (npc.transform.position, human.transform.position);
        if (dist >= 50 && dist < 100.0f) {
            Debug.Log ("Switch to Chase State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.SawHuman);

            // Stop Shooting
        }
        // Transition to patrol if human is too far
        else if (dist >= 100.0f) {
            Debug.Log("Switch to Patrol State");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.LostHuman);

            // Stop Shooting
        }

        // Check whether the zombie alive
        if (dead || health <= 0) {
            Debug.Log ("Zombie Wanna Die");
            npc.GetComponent<ZombieController>().SetTransition(ZombieTransition.NoHealth);
        }
    }

    public override void Act(GameObject human, GameObject npc) {
        // Attack Human
        npc.GetComponent<ZombieController> ().Attack ();
    }

}
