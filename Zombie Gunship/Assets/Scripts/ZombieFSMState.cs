using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ZombieFSMState {

    protected Transform[] waypoints;
    protected Vector3 desPos;
    protected ZombieFSMStateID stateID;
    protected float curRotSpeed;
    protected float curSpeed;

    protected Dictionary<ZombieTransition, ZombieFSMStateID> map = new Dictionary<ZombieTransition, ZombieFSMStateID>();

    public void AddTransition(ZombieTransition transition, ZombieFSMStateID state) {

    }

    public void DeleteTransition(ZombieTransition transition) {

    }

    public void GetOutPutState(ZombieTransition transition) {

    }

    public abstract void Reason(Transform human, Transform npc);
    public abstract void Act(Transform player, Transform npc);

}
