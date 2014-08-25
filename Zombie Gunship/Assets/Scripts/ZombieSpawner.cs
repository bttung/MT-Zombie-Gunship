using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

    public GameObject zombie;
    private float delayTime = 1.0f;
    private float respawnTimer;
    
    // Use this for initialization
    void Start () {
        respawnTimer = 0.0f;
    }
    
    // Update is called once per frame
    void Update () {
        respawnTimer += Time.deltaTime;
        if (respawnTimer > delayTime) {
            
            Vector3 pos = new Vector3(GetRandomValue(), 0 , GetRandomValue());
            
            Instantiate(zombie, pos, transform.rotation);
            respawnTimer = 0.0f;
        }
    }

    float GetRandomValue() {
        float x = Random.Range (-5, 4);
        
        if (x >= 0 && x < 4) {
            x = Random.Range (4, 5);
        } else if (x < 0 && x >= -3) {
            x = Random.Range(-4, -5);
        }

        x = x * Parameters.GRID_SIZE + Parameters.GRID_SIZE * 0.5f;

        return x;
    }
}
