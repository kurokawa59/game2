using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ボスの親クラス
public class Boss : MonoBehaviour
{
    protected float time;
    protected Rigidbody2D rb;
    protected int RandAtk;
    protected PlayerController Player;
    protected HpManager hp;
    protected int AttackFlag;//攻撃の遷移のためのフラグ
    protected int NattackCount;//通常攻撃を何回するか

    protected int BossMaxHp;//ボスの体力

    [SerializeField]protected GameObject BossProjectilePrefab;//ボスの通常の弾のプレファブ
    [SerializeField]protected GameObject BossBeamPrefab;//ビームのプレファブ
    [SerializeField]protected GameObject BeamProjectilePrefab;//ビームに付いている弾のプレファブ
    [SerializeField] protected GameObject BossTrackProjectilePrefab;//追跡する弾のプレファブ

    //サウンド系
    [SerializeField]protected AudioClip BossShotSE;
    [SerializeField]protected AudioClip BossDeadSE;
    [SerializeField]protected AudioClip BossDamagedSE;
    [SerializeField]protected AudioClip BossBeamSE;

    
    void Start()
    {
        time = 0.0f;
        NattackCount = 0;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);//反転させる
        rb = GetComponent<Rigidbody2D>();
        
    }

    

    //ボスの移動
    public void bosstranslate(float rand_x,float rand_y) {
        float x = Random.Range(-rand_x, rand_x);
        float y = Random.Range(-rand_y, rand_y);
        float x_pos = transform.position.x;
        float y_pos = transform.position.y;
        
        //x軸方向の条件とy軸方向の条件
        if (x_pos + x > -3.0f && x_pos + x < 3.0f) {
            if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                rb.velocity = new Vector2(x, y);
            } 
        } 
    }

    //ボスCの移動
    public void translate2(float rand_x, float rand_y) {
        float x = Random.Range(-rand_x, rand_x);
        float y = Random.Range(-rand_y, rand_y);
        float x_pos = transform.position.x;
        float y_pos = transform.position.y;
        //x軸方向の条件
        if (x_pos + x > -3.0f && x_pos + x < 3.0f) {
            if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                transform.Translate(x, y, 0);
            }
            
        }
    }

    public void Stop() {
        if (transform.position.x == -2.9f || transform.position.x == 2.9f || transform.position.y == 4.3f || transform.position.y == 1.5f) {
            rb.velocity = Vector2.zero;

        }
    }

    public void starttranslate(float y) {
        rb.velocity = new Vector2(0, y);
    }

    //ボスの通常攻撃
    public void BossShot(Transform boss) {
        Instantiate(BossProjectilePrefab, boss.position, boss.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        float y_pos = transform.position.y;
        //プレイヤーとの当たり判定
        if (collision.gameObject.tag == "Player") {
            Player.destroyedCount += 1;
        }
        //弾が当たったらHpが減る
        if (y_pos < 5.4) {
            if (collision.gameObject.tag == "Playerprojectile") {
                AudioSource.PlayClipAtPoint(BossDamagedSE, transform.position);
                hp.HpDown(gameObject);
                Destroy(collision.gameObject);
            }
        }

    }

}
