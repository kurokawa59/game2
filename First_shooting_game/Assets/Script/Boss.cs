using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ボスの親クラス
public class Boss : MonoBehaviour
{
    protected float time;
    protected Rigidbody2D rb;
    
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

    public void starttranslate(float y) {
        rb.velocity = new Vector2(0, y);
    }

    //ボスの通常攻撃
    public void BossShot(Transform boss) {
        Instantiate(BossProjectilePrefab, boss.position, boss.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
    }


    //以降の攻撃はラスボスでも使うのでここで実装する
    //正面にビームを撃つ(攻撃1)
    public void attack1(Transform boss) {

        Instantiate(BeamProjectilePrefab, new Vector3(boss.transform.position.x - 0.32f, boss.transform.position.y, 0), boss.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
        Instantiate(BeamProjectilePrefab, new Vector3(boss.transform.position.x + 0.33f, boss.transform.position.y, 0), boss.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
        Instantiate(BossBeamPrefab, new Vector3(boss.transform.position.x + 0.03f, boss.transform.position.y - 3.0f, 0), boss.rotation);
        AudioSource.PlayClipAtPoint(BossBeamSE, transform.position);
    }
    //分身を出してそれぞれの分身は固定の位置で攻撃してくる(攻撃2)
    public void attack2() {

    }
    //分散攻撃を連続でやってくる(攻撃3)
    public void attack3() {

    }
    //追尾してくる弾を撃つ(攻撃4)
    public void attack4() {

    }
    //瞬間移動で爆弾を置いていき爆弾が爆発すると何方向かに弾が飛んでいく(攻撃5)
    public void attack5() {

    }

    
}
