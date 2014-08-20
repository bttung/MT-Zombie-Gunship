using UnityEngine;
using System.Collections;

public class SimpleFSM : FSM {

    public enum FSMState {
        None,
        Patrol,     // Moving to the Shelter
        Chase,      // Chase near Human
        Attack,     // Attack Human
        Dead,
    }

    // Current state that the NPCZombie is reaching
    public FSMState curState;

    // Speed of the zombie
    private float curSpeed;

    // Zombie Rotation Speed
    private float curRotSpeed;

    // Bullet
    public GameObject bullet;

    // whether the Zombie is destroyed or not
    private bool dead;
    private int health;

    protected override void Initialize() {
        curState = FSMState.Patrol;
        curSpeed = 150.0f;
        curRotSpeed = 2.0f;
        dead = false;
        elapsedTime = 0.0f;
        shootRate = 3.0f;
        health = 100;

        pointList = GameObject.FindGameObjectsWithTag ("WandarPoint");

        // Set Random destination point first
        FindNextPoint ();

        // Get the target Human
        GameObject human = GameObject.FindGameObjectWithTag ("Human");

        humanTransform = human.transform;

        if (!humanTransform) {
            Debug.Log ("Human doesnt exist ... Please add human with Tag Name 'Human'");

            // Get the weapon
//            weapon = gameObject.transform;
//            bulletSpawnPoint = weapon.transform;
        }
    }

    protected override void FSMUpdate() {
        switch (curState) {
        case FSMState.Patrol:
            UpdatePatrolState();
            break;
        case FSMState.Chase:
            UpdateChaseState();
            break;
        case FSMState.Attack:
            UpdateAttackState();
            break;
        case FSMState.Dead:
            UpdateDeadState();
            break;
        }

        // Update the time
        elapsedTime += Time.deltaTime;

        // Go to dead state is no health left
        if (health <= 0) {
            curState = FSMState.Dead;
        }
    }

    protected void UpdatePatrolState() {
        // Find another random patrol point if the current point is reached
        if (Vector3.Distance (transform.position, destPos) <= 100.0f) {
            Debug.Log ("Reached to the destination point, calculating the next point");

            FindNextPoint ();
        }
        
        // Check the distance with human
        // when the distance is near, transition to chase state
        else if (Vector3.Distance (transform.position, humanTransform.position) <= 300.0f) {
            Debug.Log("Switch to Chase Position");
            curState = FSMState.Chase;
        }

        // Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation (destPos - transform.position);

        transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        // Go Forward
        transform.Translate (Vector3.forward * Time.deltaTime * curSpeed);
 
    }

    protected void UpdateChaseState() {
        // Set the target position as human position
        destPos = humanTransform.position;

        // Check the distance with human when the distance is near, transition to attack state
        float dist = Vector3.Distance (transform.position, humanTransform.position);

        if (dist <= 200.0f) {
            curState = FSMState.Attack;
        }
        // Go back to patrol is it become too far
        else if (dist >= 300.0f) {
            curState = FSMState.Patrol;
        }

        // Go Forward
        transform.Translate (Vector3.forward * Time.deltaTime * curSpeed);
    }

    protected void UpdateAttackState() {
        // Set the target position as human position
        destPos = humanTransform.position;

        // Check the distance with the player tank
        float dist = Vector3.Distance (transform.position, humanTransform.position);

        if (dist >= 200.0f && dist < 300.0f) {
            // Rotate to the target point
            Quaternion targetRotation = Quaternion.LookRotation (destPos - transform.position);
            transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);

            // Go Forward
            transform.Translate (Vector3.forward * Time.deltaTime * curSpeed);
            curState = FSMState.Attack;
        }
        // Transition to patrol if human become too far
        else if (dist >= 300.0f) {
            curState = FSMState.Patrol;
        }

        // Always turn weapon to human
        Quaternion weaponRotation = Quaternion.LookRotation (destPos, weapon.position);

        weapon.rotation = Quaternion.Slerp (weapon.rotation, weaponRotation, Time.deltaTime * curRotSpeed);

        // Shoot the bullets
        ShootBullet ();
    }

    protected void UpdateDeadState() {
        // Show the dead animation with some physics effects
        if (!dead) {
            dead = true;
            Explode();
        }
    }

    private void ShootBullet() {
        if (elapsedTime >= shootRate) {
            // Shoot the bullet
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            elapsedTime = 0.0f;
        }
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

    protected void FindNextPoint() {

    }

    // Taking Damage
    void OnCollisionEnter(Collision collision) {
        // Reduce health
        if (collision.gameObject.tag == "Bullet") {
            health -= collision.gameObject.GetComponent<Bullet>().damage;
        }
    }
}
