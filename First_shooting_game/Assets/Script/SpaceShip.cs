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
    public int score;
    private ScoreManager sm;
    public int killpoint;
    public AudioClip enemyshotSE;
    public AudioClip enemydeadSE;
    

    void Start() {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

    }
    void Update() {
        //-6より下にいったらオブジェクトを削除
        if (y_pos < -6) {
            Destroy(gameObject);
        }
    }

    //敵の移動
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
        //y座標が-2以上のときだけ移動する
        if (y_pos+y > -2) {
            transform.Translate(x, y, 0);
        }
    }

    //敵の通常の攻撃
    public void Shot(Transform enemy) {
        Instantiate(EnemyProjectilePrefab,enemy.position,enemy.rotation);
        AudioSource.PlayClipAtPoint(enemyshotSE,transform.position);
    } 

    //Nway攻撃
    public void NwayShot(Transform enemy,float angle) {
        Instantiate(EnemyProjectilePrefab, enemy.position, Quaternion.Euler(new Vector3(0.0f,0.0f,angle)));
    }

    
    void OnTriggerEnter2D(Collider2D collision) {
        //プレイヤーとの当たり判定
        if (collision.gameObject.tag == "Player") {
            Destroy(collision.gameObject);
        }

        //弾が当たったら敵と弾が消える
        if (collision.gameObject.tag == "Playerprojectile") {
            sm.AddScore(killpoint);
            AudioSource.PlayClipAtPoint(enemyshotSE,transform.position);
            Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        
    }
    
}
