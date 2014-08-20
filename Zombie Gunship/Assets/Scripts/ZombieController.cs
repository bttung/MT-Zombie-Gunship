using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[RequireComponent(typeof(Rigidbody))]
public class ZombieController : MonoBehaviour {

    public GameObject player;
    public Transform[] path;
    private ZombieFSMSystem fsm;
    private bool isExploding = false;

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

    protected void Explode() {
        float rndX = Random.Range (10.0f, 30.0f);
        float rndZ = Random.Range (10.0f, 30.0f);
        for (int i = 0; i < 3; i++) {
            rigidbody.AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
            rigidbody.velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
        }
        
        Destroy (gameObject, 1.5f);
    }
    
    // Taking Damage when Hit with Missile or Bullet.
    void OnCollisionEnter(Collision collision) {
        if (fsm.CurrentStateID == ZombieFSMStateID.Dead) {
            if (!isExploding) {
                isExploding = true;
                Explode();
                Destroy (gameObject, 4.0f);
                return;
            }
        }
        
        if (collision.gameObject.tag == "Bullet") {
            Debug.Log ("Hit with Bullet");
            fsm.CurrentState.TakeDamage(30);
        } else if (collision.gameObject.tag == "Missile") {
            Debug.Log("Hit with Missile");
            fsm.CurrentState.TakeDamage(50);
        }
    }

}
