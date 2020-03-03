using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ボスの中で最弱の敵(難易度を高くないように設定)
public class BossA : Boss {

    private int Attack6Count;//攻撃6を何回するか
    private int Attack7Count;//攻撃7を何回するか


    void Start() {
        Attack6Count = 0;
        Attack7Count = 0;
        BossMaxHp = 30;//ボスの体力

        rb = GetComponent<Rigidbody2D>();
        hp = GameObject.Find("HpManager").GetComponent<HpManager>(); ;
        Player = GameObject.Find("Player").GetComponent<PlayerController>();//Playerオブジェクトからスクリプトを取得

        hp.SetHp(BossMaxHp);
        base.starttranslate(-2.0f);
    }
    void Update() {
        //0.5秒経ったら行動する
        base.Stop();
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
            //フラグが1のときは攻撃6か攻撃7を実行
            }else if (AttackFlag == 1) {
                rb.velocity = Vector2.zero;
                //攻撃6を実行
                if (RandAtk == 0) {

                    if(Attack6Count < 10) {
                        Attack6(5);
                        Attack6Count += 1;
                        time = 0.0f;

                    } else {
                        Attack6Count = 0;
                        time = 0.0f;
                        AttackFlag = 0;
                    }
                //攻撃7を実行
                }else if (RandAtk==1) {
                    if (Attack7Count < 10) {
                        Attack7();
                        Attack7Count += 1;
                        time = 0.0f;

                    } else {
                        Attack7Count = 0;
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

    //ランダムな方向に攻撃する(攻撃6)
    public void Attack6(int NwayCount) {

        float r = Random.Range(1, NwayCount + 1);
        float angle = -(NwayCount + 1) * 5 / 2 + r * 5;
        Instantiate(BossProjectilePrefab, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

    }

    //正面にビームを発射する(攻撃7)
    //ビームの横にも弾を出してビームっぽくする
    public void Attack7() {

        Instantiate(BeamProjectilePrefab, new Vector3(transform.position.x - 0.32f, transform.position.y, 0), transform.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
        Instantiate(BeamProjectilePrefab, new Vector3(transform.position.x + 0.33f, transform.position.y, 0), transform.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
        Instantiate(BossBeamPrefab, new Vector3(transform.position.x + 0.03f, transform.position.y - 3.0f, 0), transform.rotation);
        AudioSource.PlayClipAtPoint(BossBeamSE, transform.position);
    }

    
}
