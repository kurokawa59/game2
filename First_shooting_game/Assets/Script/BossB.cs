using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossB : Boss {
    private int AttackFlag;//攻撃の遷移のためのフラグ
    private int NattackCount;//通常攻撃を何回するか
    private int Attack9Count;//攻撃9を何回するか
    private int RandAtk;//攻撃8と攻撃9のどちらを実行するか
    private int c;

    [SerializeField]private GameObject AvatarPrefab;
    [SerializeField] private GameObject TrackingProjectilePrefab;

    private PlayerController Player;
    Slider HpBar;


    void Start() {
        GameObject hp = GameObject.Find("BossHpBar");
        Player = GameObject.Find("Player").GetComponent<PlayerController>();//Playerオブジェクトからスクリプトを取得
        HpBar = hp.GetComponent<Slider>();
        rb = GetComponent<Rigidbody2D>();

        NattackCount = 0;
        c = 0;//攻撃8を1回だけ行うカウント
        Attack9Count = 0;//攻撃9を何回行っているかのカウント
        BossMaxHp = 1;//ボスBの体力

        HpBar.maxValue = BossMaxHp;
        HpBar.value = BossMaxHp;
        base.starttranslate(-2.0f);
    }
    void Update() {
        //0.5秒経ったら行動する
        int AvatarCount = GameObject.FindGameObjectsWithTag("Avatar").Length;//分身の数
        if (AvatarCount != 0) {
            gameObject.layer = LayerMask.NameToLayer("Invisible");
        } else {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        if (time > 0.5f) {
            //フラグが0のときは移動と通常攻撃だけ
            if (AttackFlag == 0) {
                //移動と通常攻撃を5回やる
                if(NattackCount < 5) {
                    Trans_and_shot();
                    NattackCount += 1;
                    time = 0.0f;

                } else {
                    AttackFlag = 1;
                    time = 0.0f;
                    NattackCount = 0;
                    RandAtk = Random.Range(0, 2);

                }
            //フラグが1のときは攻撃8か攻撃9を実行
            }else if (AttackFlag == 1) {
                rb.velocity = Vector2.zero;
                //攻撃8を実行
                if (RandAtk == 0) {
                    Attack8(gameObject.transform);
                    time = 0.0f;

                //攻撃9を実行
                }else if (RandAtk==1) {
                    if (Attack9Count < 5) {
                        Attack9(gameObject.transform);
                        Attack9Count += 1;
                        time = 0.0f;

                    } else {
                        Attack9Count = 0;
                        time = 0.0f;
                        AttackFlag = 0;
                    }
                }
            }
            
        }
        time += Time.deltaTime;
        
    }
    //移動と攻撃のまとまり
    public void Trans_and_shot() {
        base.bosstranslate(2.0f, 1.0f);

        base.bosstranslate(2.0f, 1.0f);

        base.bosstranslate(2.0f, 1.0f);

        base.BossShot(gameObject.transform);
    }

    //分身を出してそれぞれの分身は固定の位置で攻撃してくる(攻撃8)
    public void Attack8(Transform bossB) {
        if (c == 0) {
            //3つの分身を出す
            for (int i = 0; i < 3; i++) {
                float rand_x = Random.Range(-3.0f, 3.0f);
                float rand_y = Random.Range(0.0f, 4.5f);
                Instantiate(AvatarPrefab, new Vector2(rand_x, rand_y), Quaternion.identity);
            }
            c = 1;
        }else if (c == 1) {
            //分身が全滅するまで本体はランダムの方向に弾を撃つ
            if (GameObject.FindGameObjectsWithTag("Avatar").Length > 0) {
                float r = Random.Range(1, 6);
                float angle = -15 + r * 5;
                Instantiate(BossProjectilePrefab, bossB.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
                AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
            } else if (GameObject.FindGameObjectsWithTag("Avatar").Length == 0) {
                AttackFlag = 0;
                c = 0;
            }
        }
    }

    //追尾する弾を撃つ(攻撃9)
    public void Attack9(Transform bossB) {

        Instantiate(TrackingProjectilePrefab,bossB.position,Quaternion.identity);
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
                if (HpBar.value > 1) {
                    HpBar.value -= 1;
                } else if (HpBar.value == 1) {
                    Destroy(gameObject);
                }
                Destroy(collision.gameObject);
            }

        }   
        

    }
}
