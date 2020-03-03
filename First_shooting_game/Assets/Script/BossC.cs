using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//瞬間移動がテーマの敵
public class BossC : Boss {
    
    private int Attack10Count;//攻撃10を何回するか
    private int Attack11Count;//攻撃11を何回するか
    private int Attack12Count;//攻撃12を何回するか
    private int ShieldCount;

    [SerializeField]private GameObject DelayProjectilePrefab;
    [SerializeField] private GameObject BombPrefab;

    void Start() {
        Attack10Count = 0;
        Attack11Count = 0;
        Attack12Count = 0;
        BossMaxHp = 30;//ボスの体力

        rb = GetComponent<Rigidbody2D>();
        hp = GameObject.Find("HpManager").GetComponent<HpManager>();
        Player = GameObject.Find("Player").GetComponent<PlayerController>();//Playerオブジェクトからスクリプトを取得
        
        transform.position = new Vector3(0, 4.5f, 0);//最初の位置
        AttackFlag = 0;
        hp.SetHp(BossMaxHp);
    }
    
    void Update() {
        base.Stop();
        //0.5秒経ったら行動する
        ShieldCount = GameObject.FindGameObjectsWithTag("Shield").Length;//シールドの数
        if (ShieldCount != 0) {
            gameObject.layer = LayerMask.NameToLayer("Invisible");
        } else {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        if (time > 0.5f) {
            //シールドが存在しているときは移動と分散攻撃だけ
            if (ShieldCount != 0) {
                //シールドが2つとも破壊されるまでランダムな方向への攻撃を行う
                trans_and_shot();
                time = 0.0f;

                //シールドが全部壊れたときは攻撃10か攻撃11か攻撃12を実行
            } else if (ShieldCount == 0) {
                if (AttackFlag == 0) {
                    RandAtk = Random.Range(0, 3);
                    rb.velocity = Vector2.zero;
                    AttackFlag = 1;
                } else if (AttackFlag == 1) {
                    //攻撃10を実行
                    if (RandAtk == 0) {
                        if (Attack10Count < 5) {
                            Attack10();
                            Attack10Count += 1;
                            time = 0.0f;

                        } else {
                            if (time > 1.0f) {
                                Attack10Count = 0;
                                time = 0.0f;
                                AttackFlag = 0;
                            }
                        }
                        //攻撃11を実行
                    } else if (RandAtk == 1) {
                        if (Attack11Count < 3) {
                            Attack11(Attack11Count);
                            Attack11Count += 1;
                            time = 0.0f;

                        }else if(Attack11Count == 3) {
                            Attack11(Attack11Count);
                            transform.position = new Vector3(0, 5, 0);
                            Attack11Count += 1;
                            time = 0.0f;
                        } else {
                            if (time > 1.0f) {
                                Attack11Count = 0;
                                time = 0.0f;
                                AttackFlag = 0;

                            }
                        }
                        //攻撃12を実行
                    } else if (RandAtk == 2) {
                         if (Attack12Count < 2) {
                            Attack12(Attack12Count);
                            Attack12Count += 1;
                            time = 0.0f;
                        } else if (Attack12Count == 2) {
                            transform.position = new Vector3(0, 5, 0);
                            Attack12Count += 1;
                            time = 0.0f;
                        } else {
                            if (time > 1.0f) {
                                Attack12Count = 0;
                                time = 0.0f;
                                AttackFlag = 0;

                            }
                        }
                    }
                }
                
            }
            
        }
        time += Time.deltaTime;
        
    }
    

    //瞬間移動してからプレイヤーに向けた弾を撃つ攻撃する(攻撃10)
    public void Attack10() {

        base.translate2(2.0f, 1.0f);

        BossCShot();

    }

    //瞬間移動して弾を置いていき、少ししたら弾がプレイヤーに向かっていく(攻撃11)
    //奇数回目は画面右半分の位置、偶数回目は左半分の位置に移動
    public void Attack11(int AC) {


        //偶数回目
        if (AC % 2 == 0) {
            transform.position= new Vector3(-1.5f,2.95f,0);
            float x = Random.Range(-1.0f,0);
            float y = Random.Range(-1.0f, 1.0f);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > -3.0f && x_pos + x < 0) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                }

            }
        //奇数回目
        }else if (AC % 2==1) {
            transform.position = new Vector3(1.5f,2.95f,0);
            float x = Random.Range(0,1.0f);
            float y = Random.Range(-1.0f, 1.0f);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > 0 && x_pos + x < 3.0f) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                }

            }
        }
        //弾を今いる位置に配置する
        Instantiate(DelayProjectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);

    }

    //瞬間移動して爆弾を置いていき、少ししたら弾が分散する(攻撃12)
    //奇数回目は画面右半分の位置、偶数回目は左半分の位置に移動
    public void Attack12(int AC) {

        //偶数回目
        if (AC % 2 == 0) {
            transform.position = new Vector3(-1.5f, 2.95f, 0);
            float x = Random.Range(-2.0f, 0);
            float y = Random.Range(-2.0f, 2.0f);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > -3.0f && x_pos + x < 0) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                }

            }
            //奇数回目
        } else if (AC % 2 == 1) {
            transform.position = new Vector3(1.5f, 2.95f, 0);
            float x = Random.Range(0, 2.0f);
            float y = Random.Range(-2.0f, 2.0f);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > 0 && x_pos + x < 3.0f) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                }

            }
        }

        //弾を今いる位置に配置する
        Instantiate(BombPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
    }

    //追跡してくる攻撃
    public void BossCShot() {
        Instantiate(BossTrackProjectilePrefab, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
    }

    //移動と攻撃のかたまり
    public void trans_and_shot() {
        
        base.translate2(2.0f,0.5f);

        RandomShot(5);
    }

    //ランダムな方向に攻撃する(攻撃6)
    public void RandomShot( int NwayCount) {

        float r = Random.Range(1, NwayCount + 1);
        float angle = -(NwayCount + 1) * 5 / 2 + r * 5;
        Instantiate(BossProjectilePrefab, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

    }
    
}
