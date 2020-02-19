using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossA : Boss {
    public GameObject BossBeamPrefab;
    public GameObject BeamProjectilePrefab;
    private int attackflag;//攻撃の遷移のためのフラグ
    private int nattackCount;//通常攻撃を何回するか
    private int attack6Count;//攻撃6を何回するか
    private int attack7Count;//攻撃7を何回するか
    private int randatk;//攻撃6と攻撃7のどちらを実行するか
   

    void Start() {
        nattackCount = 0;
        attack6Count = 0;
        attack7Count = 0;
        rb = GetComponent<Rigidbody2D>();
        BossMaxHp = 50;
    }
    void Update() {
        //0.5秒経ったら行動する
        
        if (time > 0.5f) {
            //フラグが0のときは移動と通常攻撃だけ
            if (attackflag == 0) {
                //移動と通常攻撃を5回やる
                if(nattackCount < 5) {
                    Trans_and_shot();
                    nattackCount += 1;
                    time = 0.0f;

                } else {
                    attackflag = 1;
                    time = 0.0f;
                    nattackCount = 0;
                    randatk = Random.Range(0, 2);

                }
            //フラグが1のときは攻撃6か攻撃7を実行
            }else if (attackflag == 1) {
                rb.velocity = Vector2.zero;
                //攻撃6を実行
                if (randatk == 0) {

                    if(attack6Count < 10) {
                        attack6(gameObject.transform, 5);
                        attack6Count += 1;
                        time = 0.0f;

                    } else {
                        attack6Count = 0;
                        time = 0.0f;
                        attackflag = 0;
                    }
                //攻撃7を実行
                }else if (randatk==1) {
                    if (attack7Count < 10) {
                        attack7(gameObject.transform);
                        attack7Count += 1;
                        time = 0.0f;

                    } else {
                        attack7Count = 0;
                        time = 0.0f;
                        attackflag = 0;
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
    public void attack6(Transform bossA, int NwayCount) {

        float r = Random.Range(1, NwayCount + 1);
        float angle = -(NwayCount + 1) * 5 / 2 + r * 5;
        Instantiate(BossProjectilePrefab, bossA.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

    }

    //正面にビームを発射する(攻撃7)
    //ビームの横にも弾を出してビームっぽくする
    public void attack7(Transform bossA) {

        Instantiate(BeamProjectilePrefab, new Vector3(bossA.transform.position.x - 0.32f, bossA.transform.position.y, 0), bossA.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
        Instantiate(BeamProjectilePrefab, new Vector3(bossA.transform.position.x + 0.33f, bossA.transform.position.y, 0), bossA.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
        Instantiate(BossBeamPrefab, new Vector3(bossA.transform.position.x + 0.03f, bossA.transform.position.y - 3.0f, 0), bossA.rotation);
        AudioSource.PlayClipAtPoint(BossBeamSE, transform.position);
    }
}
