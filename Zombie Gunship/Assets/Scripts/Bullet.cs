using UnityEngine;
using System.Collections;

public class Bullet : Weapon {
   
//    public float loadTime = 1.0f;

    
    // Use this for initialization
    void Start () {
        Init ();
        loadTime = 0.0f;
//        UpdateDamageRadius (15);
    }

}
