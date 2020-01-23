using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの弾の制御
public class PlayerProjectile : ProjectileController
{
    void Start() {
        limit = 6;
    }
    void Update() {

        base.projectile(0,0.2f);
        if (transform.position.y > limit) {
            Destroy(gameObject);
        }

    }
    

}
