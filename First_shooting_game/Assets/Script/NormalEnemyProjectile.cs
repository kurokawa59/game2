using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通常の敵の弾の制御
public class NormalEnemyProjectile : ProjectileController
{
    void Start() {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        limit = -6.0f;
    }

    void Update() {
        base.projectile(0, -0.1f);
        if (transform.position.y < limit) {
            Destroy(gameObject);
        }
    }


}
