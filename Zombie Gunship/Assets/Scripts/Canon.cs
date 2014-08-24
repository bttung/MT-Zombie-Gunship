using UnityEngine;
using System.Collections;

public class Canon : Weapon {

    void Start() {
        Init ();
        UpdateDamageRadius (20.0f);
        loadTime = 2.0f;
    }

}
