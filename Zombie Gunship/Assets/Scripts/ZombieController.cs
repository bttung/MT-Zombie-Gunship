﻿using UnityEngine;
using System.Collections;

public class ZombieController : ZombieAdvancedFSM {

    protected override void Initialize() {
        // Start Doing the Finite State Machine
        ConstructFSM ();

        // Get the target Enemy
        GameObject human = GameObject.FindGameObjectWithTag ("Human");

    }

    private void ConstructFSM() {
        ZombiePatrolState patrol = new ZombiePatrolState ();
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

        AddFSMState (patrol);
        AddFSMState (chase);
        AddFSMState (attack);
        AddFSMState (dead);
    }

    public void SetTransition(ZombieTransition transition) {
        PerformTransition (transition);
    }

    public void PerformTransition(ZombieTransition trans) {
        // Check if the currentState has the transition passed as a argument

    }
}
