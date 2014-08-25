using UnityEngine;
using System.Collections;

public class HumanController : MonoBehaviour {

    private HumanFSMSystem fsm;
    public NavMeshAgent agent;
    private GameObject shelter;
    private Detonator detonator;
    public GameObject particle;
    private GameManager gameManager;
    private int count;

    public void SetTransition(HumanTransition trans) {fsm.PerformTransition(trans);}

    public void Start() {
        ConstructFSM ();
        agent = gameObject.GetComponent<NavMeshAgent> ();
        shelter = GameObject.FindGameObjectWithTag ("Shelter");
        gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
        detonator = gameObject.GetComponent<Detonator> ();
        count = 0;
    }

    
    public void FixedUpdate() {
        fsm.CurrentState.Reason (shelter.transform, gameObject.transform);
        fsm.CurrentState.Act (shelter.transform, gameObject.transform);
    }
    
    // The NPC has 4 states: Patrol, Chasing, Attack, Dead
    private void ConstructFSM() {
        HumanFleeState flee = new HumanFleeState ();
        flee.AddTransition (HumanTransition.ReachShelter, HumanFSMStateID.Sheltering);
        flee.AddTransition (HumanTransition.NoHealth, HumanFSMStateID.Dead);
        
        HumanShelterState shelter = new HumanShelterState ();
        shelter.AddTransition (HumanTransition.ReachShelter, HumanFSMStateID.Sheltering);
        
        HumanDeadState dead = new HumanDeadState ();
        dead.AddTransition (HumanTransition.NoHealth, HumanFSMStateID.Dead);
        
        fsm = new HumanFSMSystem ();
        fsm.AddState (flee);
        fsm.AddState (shelter);
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
        gameManager.IncreaseHumanDead ();
        count++;
    }


    public void TakeDamage(int damage) {
        if (fsm.CurrentStateID == HumanFSMStateID.Dead) {
            return;
        }

        fsm.CurrentState.TakeDamage (damage);
    }

    void OnTriggerEnter(Collider other) {
        // if Human deak, they die immediately, so they cannot enter the shelter
        if (fsm.CurrentState.ID == HumanFSMStateID.Dead) {
            return;
        }

        if (other.gameObject.tag == "Shelter") {
            if (fsm.CurrentState.ID == HumanFSMStateID.Sheltering) {
                // Human will not runout of the Shelter, so turn off the box Collider
                gameObject.collider.enabled = false;
            }

            SetTransition(HumanTransition.ReachShelter);
            gameManager.IncreaseHumanSaved();
        }
    }

    public bool IsDead() {
        if (fsm.CurrentState.ID == HumanFSMStateID.Dead) {
            return true;
        } else {
            return false;
        }
    }
}
