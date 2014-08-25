using UnityEngine;
using System.Collections;

public class HumanSpawner : MonoBehaviour {

    public GameObject human;
    private float delayTime = 1.5f;
    private float respawnTimer;
    
    // Use this for initialization
    void Start () {
        respawnTimer = 1;
    }
    
    // Update is called once per frame
    void Update () {
        respawnTimer += Time.deltaTime;
        if (respawnTimer > delayTime) {
            float x = Random.Range(-2, 1);
            float z = Random.Range(-2, 1);
            x = x * Parameters.GRID_SIZE + Parameters.GRID_SIZE * 0.5f;
            z = z * Parameters.GRID_SIZE + Parameters.GRID_SIZE * 0.5f;

            Vector3 pos = new Vector3(x, 1.5f , z);

            Instantiate(human, pos, transform.rotation);
            respawnTimer = 0.0f;
        }
    }
}
