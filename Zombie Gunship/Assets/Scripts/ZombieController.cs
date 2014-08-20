using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[RequireComponent(typeof(Rigidbody))]
public class ZombieController : MonoBehaviour {

    public GameObject player;
    public Transform[] path;
    private ZombieFSMSystem fsm;

    public void SetTransition(ZombieTransition trans) {fsm.PerformTransition(trans);}

    public void Start() {
        ConstructFSM ();
    }

    public void FixedUpdate() {
        fsm.CurrentState.Reason (player, gameObject);
        fsm.CurrentState.Act (player, gameObject);
    }

    // The NPC has 4 states: Patrol, Chasing, Attack, Dead
    private void ConstructFSM() {
        ZombiePatrolState patrol = new ZombiePatrolState (path);
        patrol.AddTransition (ZombieTransition.SawHuman, ZombieFSMStateID.Chasing);
        patrol.AddTransition (ZombieTransition.NoHealth, ZombieFSMStateID.Dead);

        ZombieChaseState chase = new ZombieChaseState ();
        chase.AddTransition (ZombieTransition.LostHuman, ZombieFSMStateID.Patrolling);
        chase.AddTransition (ZombieTransition.ReachHuman, ZombieFSMStateID.Atakking);
        chase.AddTransition (ZombieTransition.NoHealth, ZombieFSMStateID.Dead);

        ZombieAttackState attack = new ZombieAttackState ();
        attack.AddTransition (ZombieTransition.LostHuman, ZombieFSMStateID.Patrolling);
        attack.AddTransition (ZombieTransition.SawHuman, ZombieFSMStateID.Chasing);
        attack.AddTransition (ZombieTransition.NoHealth, ZombieFSMStateID.Dead);

        ZombieDeadState dead = new ZombieDeadState ();
        dead.AddTransition (ZombieTransition.NoHealth, ZombieFSMStateID.Dead);

        fsm = new ZombieFSMSystem ();
        fsm.AddState (patrol);
        fsm.AddState (chase);
        fsm.AddState (attack);
        fsm.AddState (dead);
    }
}
