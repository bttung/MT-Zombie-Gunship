using UnityEngine;
using System.Collections;

public class Canon : Weapon {

    void Start() {
        Init ();
        UpdateDamageRadius (15.0f);
        loadTime = 1.5f;
    }

}
