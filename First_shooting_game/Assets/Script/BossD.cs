using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossD : Boss {
    private int AttackFlag;//攻撃の遷移のためのフラグ
    private int NAttackCount;//通常攻撃のカウント
    private int Attack13Count;//攻撃13を何回するか
    private int Attack15Count;//攻撃15を何回するか
    private int RandAtk;//攻撃13と攻撃14と攻撃15のどれを実行するか
    private PlayerController Player;

    Slider HpBar;

    [SerializeField] private GameObject BossProjectilePrefab_slow2;

    void Start() {
        NAttackCount = 0;
        Attack13Count = 0;
        Attack15Count = 0;
        BossMaxHp = 1;//ボスの体力

        rb = GetComponent<Rigidbody2D>();
        GameObject hp = GameObject.Find("BossHpBar");
        Player = GameObject.Find("Player").GetComponent<PlayerController>();//Playerオブジェクトからスクリプトを取得
        HpBar = hp.GetComponent<Slider>();

        HpBar.maxValue = BossMaxHp;
        HpBar.value = BossMaxHp;
        AttackFlag = 0;
        base.starttranslate(-2.0f);
    }
    
    void Update() {
        //0.5秒経ったら行動する
        if (time > 0.5f) {
            //シールドが存在しているときは移動と分散攻撃だけ
            if (AttackFlag == 0) {
                rb.velocity = Vector2.zero;
                if(NAttackCount < 5) {
                    trans_and_shot();
                    time = 0.0f;
                    NAttackCount += 1;
                } else {
                    AttackFlag = 1;
                    NAttackCount = 0;
                    time = 0.0f;
                    RandAtk = Random.Range(2, 3);
                }

                //攻撃13か攻撃14か攻撃15を実行
            } else if (AttackFlag == 1) {
                rb.velocity = Vector2.zero;
                //攻撃13を実行
                if (RandAtk == 0) {
                    if (Attack13Count < 3) {
                        int nway = 3 + 2 * Random.Range(0, 2);
                        Attack13(nway);
                        Attack13Count += 1;
                        time = 0.0f;

                    } else {
                        if (time > 1.0f) {
                            Attack13Count = 0;
                            time = 0.0f;
                            AttackFlag = 0;
                        }
                    }
                 //攻撃14を実行
                } else if (RandAtk == 1) {
                    StartCoroutine(Attack14(300));
                    AttackFlag = 2;

                //攻撃15を実行
                } else if (RandAtk == 2) {
                    if (Attack15Count < 3) {
                        StartCoroutine(Attack15(7));
                        Attack15Count += 1;
                        time = 0.0f;
                    } else {
                        AttackFlag = 3;
                        Attack15Count = 0;
                    }
                    
                }
                
            }else if (AttackFlag == 2) {
                time = 0.0f;
            }else if (AttackFlag==3) {
                if(time > 1.5f) {
                    AttackFlag = 0;
                    time = 0.0f;
                }
            }
            
        }
        time += Time.deltaTime;
        
    }
    

    //移動してから分散攻撃(分散3か5)する(攻撃10)
    public void Attack13(int NwayCount) {

        base.bosstranslate(2.0f, 1.0f);

        for (int i = 1; i <= NwayCount; i++) {
            float angle = -(NwayCount + 1) * 7 / 2 + i * 7;
            NwayShot(angle);
        }

    }

    //らせん状に弾が飛んでいく
    IEnumerator Attack14(int c) {

        for(int i = 1; i < c; i++) {
            float angle = -(c + 1) * 10 / 2 + i * 10;
            NwayShot(angle);
            yield return new WaitForSeconds(0.02f);
        }
        AttackFlag = 0;
    }

    //弾を発射したら数秒後に2段階分裂する
    IEnumerator Attack15(int NwayCount) {

        //最初の弾
        float r1 = Random.Range(1, NwayCount + 1);
        float angle1 = -(NwayCount + 1) * 5 / 2 + r1 * 5;
        GameObject p = Instantiate(BossProjectilePrefab_slow2, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle1)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

        yield return new WaitForSeconds(0.2f);

        //最初の弾からの1段階の分裂
        float r2 = Random.Range(1, NwayCount + 1);
        float angle2 = -(NwayCount + 1) * 5 / 2 + r2 * 5;
        GameObject p2=Instantiate(BossProjectilePrefab_slow2, p.transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle2)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

        yield return new WaitForSeconds(0.5f);

        //1段階で分裂したそれぞれの弾が分裂
        float r3 = Random.Range(1, NwayCount + 1);
        float angle3 = -(NwayCount + 1) * 5 / 2 + r3 * 5;
        Instantiate(BossProjectilePrefab_slow2, p.transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle3)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

        float r4 = Random.Range(1, NwayCount + 1);
        float angle4 = -(NwayCount + 1) * 5 / 2 + r4 * 5;
        Instantiate(BossProjectilePrefab_slow2, p2.transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle4)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

    }

    //移動と通常攻撃のかたまり
    public void trans_and_shot() {
        
        base.bosstranslate(2.0f,1.0f);

        base.BossShot(gameObject.transform);

    }

    //分散攻撃
    public void NwayShot(float angle) {
        
        Instantiate(BossProjectilePrefab, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
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
                if (HpBar.value > 0) {
                    HpBar.value -= 1;
                } else if (HpBar.value == 0) {
                    Destroy(gameObject);
                }
                Destroy(collision.gameObject);
            }
        }
    

    }
}
