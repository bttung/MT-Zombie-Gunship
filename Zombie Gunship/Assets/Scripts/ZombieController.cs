using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[RequireComponent(typeof(Rigidbody))]
public class ZombieController : MonoBehaviour {

    private GameObject shelter;
    public GameObject[] humanList;
    public GameObject human;
    public NavMeshAgent agent;
    private ZombieFSMSystem fsm;
    private GameManager gameManager;
    private Detonator detonator;
    public GameObject particle;
    private int count;

    public void SetTransition(ZombieTransition trans) {fsm.PerformTransition(trans);}

    public void Start() {
        ConstructFSM ();
        agent = gameObject.GetComponent<NavMeshAgent> ();
        detonator = gameObject.GetComponent<Detonator> ();
        shelter = GameObject.FindGameObjectWithTag ("Shelter");
        gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
        count = 0;
    }

    public void FixedUpdate() {
        FindApproriateHuman();

        if (human == null) {
//            Debug.Log("Zombie win the game");
            return;
        }

        fsm.CurrentState.Reason (shelter.transform, human.transform, gameObject.transform);
        fsm.CurrentState.Act (shelter.transform, human.transform, gameObject.transform);
    }

    void FindHuman() {
        humanList = GameObject.FindGameObjectsWithTag ("Human");
    }

    void FindApproriateHuman() {
        FindHuman ();
        float min = 100000f;
        float dist;
        for (int i = 0; i < humanList.Length; i++) {
            dist = Vector3.Distance(gameObject.transform.position, humanList[i].transform.position);
            if (dist < min) {
                min = dist;
                human = humanList[i];
            }
        }
    }


    // The NPC has 4 states: Patrol, Chasing, Attack, Dead
    private void ConstructFSM() {
        ZombiePatrolState patrol = new ZombiePatrolState ();
        patrol.AddTransition (ZombieTransition.SawHuman, ZombieFSMStateID.Chasing);
        patrol.AddTransition (ZombieTransition.NoHealth, ZombieFSMStateID.Dead);
        patrol.AddTransition (ZombieTransition.ReachShelter, ZombieFSMStateID.Win);

        ZombieChaseState chase = new ZombieChaseState ();
        chase.AddTransition (ZombieTransition.LostHuman, ZombieFSMStateID.Patrolling);
        chase.AddTransition (ZombieTransition.ReachHuman, ZombieFSMStateID.Atakking);
        chase.AddTransition (ZombieTransition.NoHealth, ZombieFSMStateID.Dead);
        chase.AddTransition (ZombieTransition.ReachShelter, ZombieFSMStateID.Win);

        ZombieAttackState attack = new ZombieAttackState ();
        attack.AddTransition (ZombieTransition.LostHuman, ZombieFSMStateID.Patrolling);
        attack.AddTransition (ZombieTransition.SawHuman, ZombieFSMStateID.Chasing);
        attack.AddTransition (ZombieTransition.NoHealth, ZombieFSMStateID.Dead);
        attack.AddTransition (ZombieTransition.ReachShelter, ZombieFSMStateID.Win);

        ZombieDeadState dead = new ZombieDeadState ();
        dead.AddTransition (ZombieTransition.NoHealth, ZombieFSMStateID.Dead);

        fsm = new ZombieFSMSystem ();
        fsm.AddState (patrol);
        fsm.AddState (chase);
        fsm.AddState (attack);
        fsm.AddState (dead);
    }

    public void Explode() {

        if (count > 0) {
            return;
        }

        // Dont know why detonator dont work on the device
        detonator.gameObject.transform.position = gameObject.transform.position;
        detonator.Explode ();

        // Explode Effect by particle
        // Instantiate(particle, gameObject.transform.position, Quaternion.identity);

        Destroy (gameObject, 1.0f);

        gameManager.IncreasedZombieKilled ();
        count++;
    }

    public void Attack() {
        // Check if human died
        if (human == null || human.gameObject.GetComponent<HumanController>().IsDead()) {
            return;
        }

        // Attack Human
        // Start the Attack Coroutine ...
        human.GetComponent<HumanController> ().TakeDamage (100);
    }

    
    public void TakeDamage(int damage) {
        if (fsm.CurrentStateID == ZombieFSMStateID.Dead) {
            return;
        }
        
        fsm.CurrentState.TakeDamage (damage);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Shelter") {
            SetTransition(ZombieTransition.ReachShelter);
            gameManager.IncreaseZombieInShelter();
        }
    }

}
