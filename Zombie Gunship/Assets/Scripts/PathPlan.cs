using UnityEngine;
using System.Collections;

public class PathPlan : MonoBehaviour {

    public Transform target;
    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination (target.position);
	}
}
