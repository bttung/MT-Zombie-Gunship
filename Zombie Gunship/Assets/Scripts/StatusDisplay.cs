using UnityEngine;
using System.Collections;

public class StatusDisplay : MonoBehaviour {

    public GUIText human;
    public GUIText zombie;
    private int humanSaved = 0;
    private int humanDied = 0;
    private int zombieKilled = 0;
    private int zombieInShelter = 0;


	// Use this for initialization
	void Start () {
        human.text = "";
        zombie.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void RefreshHumanText() {
        human.text = humanSaved +  "Human In Shelter";
    }

    public void RefreshMessage(string message) {
        human.text = message;
    }

    void RefreshZombieText() {
        zombie.text = zombieInShelter +  "Zombie In Shelter";
    }

    void RefreshText() {
        human.text = humanSaved +  " Human In Shelter " + zombieInShelter +  "Zombie In Shelter";
    }

    public void IncreaseHumanSaved() {
        humanSaved++;
//        Debug.Log ("human: " + humanSaved);
//        RefreshHumanText ();
        RefreshText ();
    }

    public void IncreaseZombieInShelter() {
        zombieInShelter++;
//        Debug.Log ("zombie " + zombieInShelter);
//        RefreshZombieText ();
        RefreshText ();
    }
}
