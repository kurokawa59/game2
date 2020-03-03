using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵キャラ(雑魚)の親クラス
public class SpaceShip : MonoBehaviour
{
    protected float time;
    [SerializeField]private GameObject EnemyProjectilePrefab;
    private ScoreManager sm;
    public int killpoint;
    protected int AttackFlag;//攻撃の切り替えのためのフラッグ
    [SerializeField]private AudioClip enemyshotSE;//発射音
    [SerializeField]private AudioClip enemydeadSE;//倒された時の音
    private Rigidbody2D rb;
    private PlayerController player;
    

    void Start() {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        sm = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        AttackFlag = 0;
    }
    void Update() {
        //-6より下にいったらオブジェクトを削除
        if (transform.position.y < -6.0f) {
            Destroy(gameObject);
        }
    }

    //敵の移動
    public void translate(float rand_x,float rand_y) {
        float x = Random.Range(-rand_x, rand_x);
        float y = Random.Range(-rand_y, 0.0f);
        float x_pos = transform.position.x;
        //x軸方向の条件
        if (x_pos + x > -3.0f && x_pos + x < 3.0f) {
            rb.velocity = new Vector2(x, y);
        }
    }

    //瞬間移動する敵だけの移動
    public void translate2(int rand_x, int rand_y) {
        float x = Random.Range(-rand_x, rand_x);
        float y = Random.Range(-rand_y, 0.0f);
        float x_pos = transform.position.x;
        //x軸方向の条件
        if (x_pos + x > -3.0f && x_pos + x < 3.0f) {
            transform.Translate(x, y,0);
        }
    }
    //止まる
    public void stop() {
        rb.velocity = Vector2.zero;
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
        float y_pos = transform.position.y;
        //プレイヤーとの当たり判定
        if (collision.gameObject.tag == "Player") {
            player.destroyedCount += 1;
        }

        //弾が当たったら敵と弾が消える
        if(y_pos < 5.4) {
            if (collision.gameObject.tag == "Playerprojectile") {
                sm.AddScore(killpoint);
                AudioSource.PlayClipAtPoint(enemydeadSE, transform.position);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        
    }
    
}
