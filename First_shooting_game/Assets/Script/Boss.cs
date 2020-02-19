using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    protected float time;
    protected Rigidbody2D rb;
    public GameObject BossProjectilePrefab;//ボスの通常の弾のプレファブ

    //サウンド系
    public AudioClip BossShotSE;
    public AudioClip BossDeadSE;
    public AudioClip BossDamagedSE;
    public AudioClip BossBeamSE;

    private PlayerController Player;
    protected float BossMaxHp;
    private HpBarController HpBar;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);//反転させる
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player").GetComponent<PlayerController>();//Playerオブジェクトからスクリプトを取得
        HpBar = GameObject.Find("HPBarController").GetComponent<HpBarController>();//HPバーをいじるためにHpControllerオブジェクトからスクリプトを取得
        HpBar.MaxHp = BossMaxHp;
        HpBar.CurrentHp = BossMaxHp;
    }

    

    //ボスの移動
    public void bosstranslate(float rand_x,float rand_y) {
        float x = Random.Range(-rand_x, rand_x);
        float y = Random.Range(-rand_y, rand_y);
        float x_pos = transform.position.x;
        float y_pos = transform.position.y;
        
        //x軸方向の条件とy軸方向の条件
        if (x_pos + x > -3.0f && x_pos + x < 3.0f) {
            if (y_pos + y < 4.4f && y_pos + y > 3.0f) {
                rb.velocity = new Vector2(x, y);

            } 
        } 
    }

    //ボスの通常攻撃
    public void BossShot(Transform boss) {
        Instantiate(BossProjectilePrefab, boss.position, boss.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
    }



    //以降の攻撃はラスボスでも使うのでここで実装する
    //正面にビームを撃つ(攻撃1)
    public void attack1() {

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

    void OnTriggerEnter2D(Collider2D collision) {
        float y_pos = transform.position.y;
        //プレイヤーとの当たり判定
        if (collision.gameObject.tag == "Player") {
            Player.destroyedCount += 1;
        }

        //弾が当たったら敵と弾が消える
        if (y_pos < 5.4) {
            if (collision.gameObject.tag == "Playerprojectile") {
                AudioSource.PlayClipAtPoint(BossDamagedSE, transform.position);
                HpBar.CurrentHp -= 1 ;
                Destroy(collision.gameObject);
            }
        }

    }

}
