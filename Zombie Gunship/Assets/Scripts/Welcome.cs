using UnityEngine;
using System.Collections;

public class Welcome : MonoBehaviour {

    public Texture2D welcome;

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Portrait;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        Rect rect = new Rect (Screen.width / 2 - welcome.width / 2, Screen.height / 2 - welcome.height / 2, welcome.width, welcome.height);
        GUI.DrawTexture (rect, welcome);

        if (GUI.Button (rect, "", new GUIStyle ())) {
            Application.LoadLevel("Play");
        }
    }
}
