using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GUIText summary;
    public Texture2D win;
    public Texture2D lose;

    private int humanSaved = 0;
    private int humanDied = 0;
    private int zombieKilled = 0;
    private int zombieInShelter = 0;

    private bool refreshResult;

	// Use this for initialization
	void Start () {
        summary.text = "";
        refreshResult = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (refreshResult) {
            RefreshText ();
            refreshResult = false;
        }
    }

    void OnGUI() {
//        if (zombieInShelter > 0 || humanDied >= 10) {
//            DrawTexture(lose);
//            return;
//        }
//
//        if (zombieKilled > 15) {
//            DrawTexture(win);
//            return;
//        }
    }

    void RefreshText() {
        summary.text = humanSaved +  " Human Saved, " + humanDied +  " Human Died, " + zombieKilled + " Zombie Killed, " + zombieInShelter + " Zombie in Shelter";
    }

    public void IncreaseHumanSaved() {
        humanSaved++;
        refreshResult = true;
    }

    public void IncreaseHumanDead () {
        humanDied++;
        refreshResult = true;
    }

    public void IncreaseZombieInShelter() {
        zombieInShelter++;
        refreshResult = true;
    }

    public void IncreasedZombieKilled() {
        zombieKilled++;
        refreshResult = true;
    }

//    public void DrawTexture(Texture2D texture) {
//        Rect rect = new Rect (Screen.width / 2 - texture.width / 2, Screen.height / 2 - texture.height / 2, texture.width, texture.height);
//        GUI.DrawTexture (rect, texture);
//        
//        if (GUI.Button (rect, "", new GUIStyle ())) {
//            Application.LoadLevel("Level");
//        }
//    }
}
