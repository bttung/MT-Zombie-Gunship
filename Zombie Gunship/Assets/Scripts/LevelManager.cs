using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Portrait;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        DrawButton (new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 200), "Welcome");
        DrawButton (new Rect(Screen.width / 2 - 100, Screen.height / 2 + 200, 200, 200), "Play");
    }

    public void DrawButton(Rect rect, string name) {        
        if (GUI.Button (rect, name, new GUIStyle ())) {
            Application.LoadLevel(name);
        }
    }
}
