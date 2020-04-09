using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//今までの攻撃を改良した攻撃などすることがテーマ
public class LastBoss : Boss
{
    private int Attack1Count;//攻撃1を何回するか
    private int Attack3Count;//攻撃3を何回するか
    private int Attack4Count;//攻撃4を何回するか
    private int Attack5Count;//攻撃5を何回するか

    [SerializeField] private GameObject AvatarPrefab;
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private GameObject SpiralBombPrefab;
    [SerializeField] private GameObject BossProjectilePrefab_slow2;
    [SerializeField] private GameObject DelayProjectilePrefab;
    [SerializeField] private GameObject BossProjectilePrefab_slow;

    private int c;


    void Start() {
        Attack1Count = 0;
        Attack3Count = 0;
        Attack4Count = 0;
        Attack5Count = 0;
        BossMaxHp = 100;//ボスの体力

        rb = GetComponent<Rigidbody2D>();
        hp = GameObject.Find("HpManager").GetComponent<HpManager>();
        Player = GameObject.Find("Player").GetComponent<PlayerController>();//Playerオブジェクトからスクリプトを取得
        
        AttackFlag = 0;
        c = 0;
        hp.SetHp(BossMaxHp);
        base.starttranslate(-2.0f);
    }

    void Update() {
        base.Stop();
        int AvatarCount = GameObject.FindGameObjectsWithTag("Avatar").Length;//分身の数
        if (AvatarCount != 0) {
            gameObject.layer = LayerMask.NameToLayer("Invisible");
        } else {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        //0.5秒経ったら行動する
        if (time > 0.5f) {
            if (AttackFlag == 0) {
                rb.velocity = Vector2.zero;
                if (NattackCount < 5) {
                    
                    trans_and_shot2();
                   
                    time = 0.0f;
                    NattackCount += 1;
                } else {
                    AttackFlag = 1;
                    NattackCount = 0;
                    time = 0.0f;
                    RandAtk = Random.Range(0, 5);
                }

                //攻撃1か攻撃2か攻撃3か攻撃4か攻撃5を実行
            } else if (AttackFlag == 1) {
                rb.velocity = Vector2.zero;
                //攻撃1を実行
                if (RandAtk == 0) {
                    if (Attack1Count < 10) {
                        Attack1(7);
                        Attack1Count += 1;
                        time = 0.0f;

                    } else {
                        if (time > 1.0f) {
                            Attack1Count = 0;
                            time = 0.0f;
                            AttackFlag = 0;
                        }
                    }
                    //攻撃2を実行
                } else if (RandAtk == 1) {
                    Attack2();
                    time = 0.0f;

                    //攻撃3を実行
                } else if (RandAtk == 2) {
                    if (Attack3Count < 4) {
                        Attack3(Attack3Count);
                        Attack3Count += 1;
                        time = 0.0f;

                    } else if (Attack3Count == 4) {
                        transform.position = new Vector3(0, 5, 0);
                        Attack3Count += 1;
                        time = 0.0f;
                    } else {
                        if (time > 1.0f) {
                            Attack3Count = 0;
                            time = 0.0f;
                            AttackFlag = 0;
                        }

                    }
                //攻撃4を実行
                } else if (RandAtk == 3) {
                    if (Attack4Count < 2) {
                        Attack4(Attack4Count);
                        Attack4Count += 1;
                        time = 0.0f;
                    } else if (Attack4Count == 2) {
                        transform.position = new Vector3(0, 5, 0);
                        Attack4Count += 1;
                        time = 0.0f;
                    } else {
                        Attack4Count = 0;
                        AttackFlag = 2;
                        time = 0.0f;
                    }

                    //攻撃5を実行
                } else if (RandAtk == 4) {
                    
                    if (Attack5Count < 4) {
                        
                        Attack5(Attack5Count);
                        Attack5Count += 1;
                        time = 0.0f;

                    } else if (Attack5Count == 4) {

                        transform.position = new Vector3(0, 5, 0);
                        Attack5Count += 1;
                        time = 0.0f;
                    } else {
                        
                        if (time > 1.0f) {
                            Attack5Count = 0;
                            time = 0.0f;
                            AttackFlag = 0;

                        }
                    }
                }
            } else if (AttackFlag == 2) {
                time = 0.0f;
            } else if (AttackFlag == 3) {
                if (time > 1.5f) {
                    AttackFlag = 0;
                    time = 0.0f;
                }
            }

        }
        time += Time.deltaTime;

    }

    //正面にビームを撃つ+ランダムの方向に弾を撃つ(攻撃1)
    public void Attack1(int NwayCount) {

        //ビームの発射
        Instantiate(BeamProjectilePrefab, new Vector3(transform.position.x - 0.32f, transform.position.y, 0), transform.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
        Instantiate(BeamProjectilePrefab, new Vector3(transform.position.x + 0.33f, transform.position.y, 0), transform.rotation);
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
        Instantiate(BossBeamPrefab, new Vector3(transform.position.x + 0.03f, transform.position.y - 3.0f, 0), transform.rotation);
        AudioSource.PlayClipAtPoint(BossBeamSE, transform.position);

        //ランダムの方向への攻撃
        float r = Random.Range(1, NwayCount + 1);
        float angle = -(NwayCount + 1) * 5 / 2 + r * 5;
        Instantiate(BossProjectilePrefab, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
    }
    //分身を出してそれぞれの分身は固定の位置で攻撃してくる(攻撃2)
    public void Attack2() {
        if (c == 0) {
            //4つの分身を出す
            for (int i = 0; i < 4; i++) {
                float rand_x = Random.Range(-3.0f, 3.0f);
                float rand_y = Random.Range(0.0f, 4.5f);
                Instantiate(AvatarPrefab, new Vector2(rand_x, rand_y), Quaternion.identity);
            }
            c = 1;
        } else if (c == 1) {
            //分身が全滅するまで本体はランダムの方向に弾を撃つ
            if (GameObject.FindGameObjectsWithTag("Avatar").Length > 0) {
                float r = Random.Range(1, 6);
                float angle = -15 + r * 5;
                Instantiate(BossProjectilePrefab, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
                AudioSource.PlayClipAtPoint(BossShotSE, transform.position);
            //分身が全滅したら
            } else if (GameObject.FindGameObjectsWithTag("Avatar").Length == 0) {
                AttackFlag = 0;
                c = 0;
            }
        }
    }

    //瞬間移動で弾を置いていき時間が経つとプレイヤー方向に弾が飛んでいく(攻撃3)
    public void Attack3(int AC) {
        //偶数回目
        if (AC % 2 == 0) {
            transform.position = new Vector3(-1.5f, 4.0f, 0);
            float x = Random.Range(-1.0f, 0);
            float y = Random.Range(-0.5f, 0);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > -3.0f && x_pos + x < 0) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                    //弾を今いる位置に配置する
                    GameObject p1 = Instantiate(DelayProjectilePrefab, new Vector3(transform.position.x, transform.position.y - 0.55f, 0), Quaternion.identity);
                    StartCoroutine(Split(7,p1,1.5f,0.5f));
                }

            }
            //奇数回目
        } else if (AC % 2 == 1) {
            transform.position = new Vector3(1.5f, 4.0f, 0);
            float x = Random.Range(0, 1.0f);
            float y = Random.Range(-0.5f,0);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > 0 && x_pos + x < 3.0f) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                    //弾を今いる位置に配置する
                    GameObject p2 = Instantiate(DelayProjectilePrefab, new Vector3(transform.position.x, transform.position.y - 0.55f, 0), Quaternion.identity);
                    StartCoroutine(Split(7,p2,1.5f,0.5f));
                }

            }
        }
        
    }

    //爆弾を2つ出して全方位の弾幕をそれぞれが出す(攻撃4)
    public void Attack4(int AC2) {
        //偶数回目
        if (AC2 % 2 == 0) {
            transform.position = new Vector3(-1.5f, 2.95f, 0);
            float x = Random.Range(-0.5f, 0);
            float y = Random.Range(-1.0f, 1.0f);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > -3.0f && x_pos + x < 0) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                    //弾を今いる位置に配置する
                    GameObject b1=Instantiate(SpiralBombPrefab, new Vector3(transform.position.x,transform.position.y-0.55f,0), Quaternion.identity);
                    StartCoroutine(Spiral(10, 100, b1));
                    
                }

            }
        //奇数回目
        } else if (AC2 % 2 == 1) {
            transform.position = new Vector3(1.5f, 2.95f, 0);
            float x = Random.Range(0, 0.5f);
            float y = Random.Range(-1.0f, 1.0f);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > 0 && x_pos + x < 3.0f) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                    //弾を今いる位置に配置する
                    GameObject b2=Instantiate(SpiralBombPrefab, new Vector3(transform.position.x,transform.position.y-0.55f, 0), Quaternion.identity);
                    StartCoroutine(Spiral(10, 100, b2));
                    
                }

            }
        }
        

    }

    //瞬間移動で爆弾を置いていき爆弾が爆発すると何方向かに弾が飛んでいき、分裂する(攻撃5)
    //奇数回目は画面右半分の位置、偶数回目は左半分の位置に移動
    public void Attack5(int AC) {
        
        //偶数回目
        if (AC % 2 == 0) {
            transform.position = new Vector3(-1.5f, 2.95f, 0);
            float x = Random.Range(-1.0f, 0);
            float y = Random.Range(-1.0f, 1.0f);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > -3.0f && x_pos + x < 0) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                    //弾を今いる位置に配置する
                    Instantiate(BombPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
                    
                    

                }

            }
            //奇数回目
        } else if (AC % 2 == 1) {
            transform.position = new Vector3(1.5f, 2.95f, 0);
            float x = Random.Range(0, 1.0f);
            float y = Random.Range(-1.0f, 1.0f);
            float x_pos = transform.position.x;
            float y_pos = transform.position.y;
            //x軸方向の条件
            if (x_pos + x > 0 && x_pos + x < 3.0f) {
                if (y_pos + y < 4.4f && y_pos + y > 1.5f) {
                    transform.Translate(x, y, 0);
                    //弾を今いる位置に配置する
                    Instantiate(BombPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
                    
                    

                }

            }
        }

    }

    //らせん状の弾を出す
    IEnumerator Spiral(int NwayRange, int co, GameObject g) {
        yield return new WaitForSeconds(2);
        for (int i = 1; i < co; i++) {
            float angle = -(co + 1) * NwayRange / 2 + i * NwayRange;
            NwayShot2(angle, g.transform);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(g);
        AttackFlag = 0;
    }

    //弾を発射して数秒後に1段階分裂する
    IEnumerator Split(int NwayCount,GameObject p,float t1,float t2) {

        yield return new WaitForSeconds(t1);

        //最初の分裂
        float r1 = Random.Range(1, NwayCount + 1);
        float angle1 = -(NwayCount + 1) * 5 / 2 + r1 * 5;
        GameObject p1 = Instantiate(BossProjectilePrefab_slow2, p.transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle1)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

        yield return new WaitForSeconds(t2);

        //1段階で分裂したそれぞれの弾が分裂
        float r3 = Random.Range(1, NwayCount + 1);
        float angle3 = -(NwayCount + 1) * 5 / 2 + r3 * 5;
        Instantiate(BossProjectilePrefab_slow2, p.transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle3)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

        float r4 = Random.Range(1, NwayCount + 1);
        float angle4 = -(NwayCount + 1) * 5 / 2 + r4 * 5;
        Instantiate(BossProjectilePrefab_slow2, p1.transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle4)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);


    }

    //移動と通常攻撃のかたまり
    public void trans_and_shot1() {

        base.bosstranslate(2.0f, 1.0f);

        base.BossShot(gameObject.transform);

    }

    //移動とランダムの方向への攻撃のかたまり
    public void trans_and_shot2() {

        base.bosstranslate(2.0f, 1.0f);

        float r = Random.Range(1, 8);
        float angle = -20 + r * 5;
        Instantiate(BossProjectilePrefab, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
        AudioSource.PlayClipAtPoint(BossShotSE, transform.position);

    }

    //分散攻撃
    public void NwayShot2(float angle,Transform Bomb) {

        Instantiate(BossProjectilePrefab_slow, Bomb.transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
        AudioSource.PlayClipAtPoint(BossShotSE, Bomb.transform.position);
    }

    
}
