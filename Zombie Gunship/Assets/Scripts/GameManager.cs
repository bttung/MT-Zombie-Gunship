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

    // Rotation for game Result
    private GUIStyle myStyle;
    private float rotateAngle;
    private Vector2 pivotPoint;

	// Use this for initialization
	void Start () {
        summary.text = "";
        rotateAngle = 270;
        pivotPoint = new Vector2 (70, Screen.height - 70);
        myStyle = new GUIStyle ();
        myStyle.normal.textColor = Color.black;
        myStyle.fontSize = 30;
	}

    void OnGUI() {

        if (zombieInShelter > 0 || humanDied >= 3) {
            DrawTexture(lose);
//            return;
        }
        
        if (zombieKilled > 100) {
            DrawTexture(win);
//            return;
        }

        GUIUtility.RotateAroundPivot (rotateAngle, pivotPoint);
        GUI.Label(new Rect(50, Screen.height - 120, 120, 40), humanSaved +  " Human Saved", myStyle);
        GUI.Label(new Rect(50, Screen.height - 90, 120, 40), humanDied +  " Human Died", myStyle);
        GUI.Label(new Rect(50, Screen.height - 60, 120, 40), zombieKilled + " Zombie Killed", myStyle);
        GUI.Label(new Rect(50, Screen.height - 30, 150, 40), zombieInShelter + " Zombie in Shelter", myStyle);

    }

    void RefreshText() {
        summary.text = humanSaved +  " Human Saved, " + humanDied +  " Human Died, " + zombieKilled + " Zombie Killed, " + zombieInShelter + " Zombie in Shelter";
    }

    public void IncreaseHumanSaved() {
        humanSaved++;
    }

    public void IncreaseHumanDead () {
        humanDied++;
    }

    public void IncreaseZombieInShelter() {
        zombieInShelter++;
    }

    public void IncreasedZombieKilled() {
        zombieKilled++;
    }

    public void DrawTexture(Texture2D texture) {
        Rect rect = new Rect (Screen.width / 2 - texture.width / 2, Screen.height / 2 - texture.height / 2, texture.width, texture.height);
        GUI.DrawTexture (rect, texture);
        
        if (GUI.Button (rect, "", new GUIStyle ())) {
            Application.LoadLevel("Welcome");
        }
    }
}
