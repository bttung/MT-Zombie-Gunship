using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ZombieFSMState : MonoBehaviour{

    public ZombieFSMStateID stateID;
    protected float curRotSpeed;
    protected float curSpeed;
    protected Vector3 desPos;

    // Bullet
    public GameObject bullet;
    protected float shootRate;
    protected float elapsedTime;

    protected Dictionary<ZombieTransition, ZombieFSMStateID> map = new Dictionary<ZombieTransition, ZombieFSMStateID>();

    public ZombieFSMStateID GetOutputState(ZombieTransition trans) {
        ZombieFSMStateID id;
        bool hasValue = map.TryGetValue (trans, out id);
        if (hasValue) {
            return id;
        } else {
            return ZombieFSMStateID.None;
        }
    }

    public void AddTransition(ZombieTransition transition, ZombieFSMStateID state) {
        map.Add (transition, state);
    }

    public void DeleteTransition(ZombieTransition transition) {
        map.Remove (transition);
    }

    public abstract void Reason(Transform human, Transform npc);
    public abstract void Act(Transform player, Transform npc);

}
