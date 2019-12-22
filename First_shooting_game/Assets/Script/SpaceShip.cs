using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵キャラの親クラス
public class SpaceShip : MonoBehaviour
{
    public float time = 0.0f;
    public float x;
    public float y;
    public float x_pos = 0.0f;
    public float y_pos = 0.0f;
    public GameObject EnemyProjectilePrefab;
    void Start() {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);

    }
    void Update() {
        //-6より下にいったらオブジェクトを削除
        if (y_pos < -6) {
            Destroy(gameObject);
        }
    }

    public void translate(float rand_x,float rand_y) {
        x_pos = transform.position.x;
        y_pos = transform.position.y;
        x = Random.Range(-rand_x, rand_x);
        y = Random.Range(-rand_y, 0.0f);
        if (x_pos + x <= -3.0f) {
            x = Random.Range(0.0f, rand_x);

        } else if (x_pos + x >= 3.0f) {
            x = Random.Range(-rand_x, 0.0f);
        }
        transform.Translate(x, y, 0);
    }

    //敵の通常の攻撃
    public void Shot(Transform enemy) {
        Instantiate(EnemyProjectilePrefab,enemy.position,enemy.rotation);
    } 

    //Nway攻撃
    public void NwayShot(Transform enemy,float angle) {
        Instantiate(EnemyProjectilePrefab, enemy.position, Quaternion.Euler(new Vector3(0.0f,0.0f,angle)));
    }

    //プレイヤーとの当たり判定
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(collision.gameObject);
        }
    }
    
}
