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
    //弾が当たったら敵と弾が消える
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "TelepoEnemy" || collision.gameObject.tag == "NormalEnemy" || collision.gameObject.tag == "DispersionEnemy") {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
