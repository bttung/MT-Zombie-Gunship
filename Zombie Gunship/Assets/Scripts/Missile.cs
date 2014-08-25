using UnityEngine;
using System.Collections;

public class Missile : Weapon {
    
    void Start() {
        Init ();
        UpdateDamageRadius (20.0f);
        loadTime = 2.0f;
    }
    
}

