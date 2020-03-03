using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ビームのスクリプト
public class BeamController : ProjectileController
{
    private float ylimit;

    void Start() {
        ylimit = -10.0f;
    }
    void Update()
    {
        base.projectile(0, -0.1f);
        if (transform.position.y < ylimit) {
            Destroy(gameObject);
        }
    }
}
