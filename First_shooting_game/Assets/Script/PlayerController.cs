using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//プレイヤーの制御
public class PlayerController : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    private float time;
    public int destroyedCount;//倒された数

    public static PlayerController Player_Instance;
    public AudioClip deadSE;
    public AudioClip shotSE;
    private Renderer renderer;
    private Animator anim;

    public GameObject[] hpIcon;

    void Start() {
        Player_Instance = this;
        anim = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        time = 0.0f;
        destroyedCount = 0;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        //プレイヤーの動き(-3.1 < x < 3.1の範囲で動く)
        /*
        //タッチ判定
        if (Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width * 0.5f) {

                //画面外に行かないようにする
                if (-3.1 < transform.position.x) {
                    transform.Translate(-0.05f, 0, 0);
                    if (touch.phase == TouchPhase.Began) {
                        //押した時
                        anim.SetBool("left_long", true);
                    }
                    if (touch.phase == TouchPhase.Moved) {
                        //長押しの時
                        
                        anim.SetBool("left_long", true);
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        //指を離した時
                        anim.SetBool("left_long",false);
                        
                    }

                } else {
                    if (touch.phase == TouchPhase.Moved) {
                        anim.SetBool("left_long", true);
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        anim.SetBool("left_long", false);
                    }
                    transform.Translate(0, 0, 0);
                }
            } else if (touch.position.x > Screen.width * 0.5f) {
                //画面外に行かないようにする
                if (3.1 > transform.position.x) {
                    transform.Translate(0.05f, 0, 0);
                    if (touch.phase == TouchPhase.Began) {
                        //押した時
                        anim.SetBool("right_long", true);
                    }
                    if (touch.phase == TouchPhase.Moved) {
                        //長押しの時
                        
                        anim.SetBool("right_long", true);
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        //指を離した時
                        anim.SetBool("right_long",false);
                        
                    }


                } else {
                    if (touch.phase == TouchPhase.Moved) {
                        anim.SetBool("right_long", true);
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        anim.SetBool("right_long",false);
                    }
                    transform.Translate(0, 0, 0);
                    
                }
            }

        }
        */
        

        
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if(-3.1 < transform.position.x) {
                transform.Translate(-0.05f, 0, 0);
                anim.SetBool("is_left", true);
                anim.SetBool("left_long",true);
            } else{
                transform.Translate(0, 0, 0);
            }
        }else if (Input.GetKey(KeyCode.RightArrow)) {
            if (3.1 > transform.position.x) {
                transform.Translate(0.05f, 0, 0);
                anim.SetBool("is_right", true);
                anim.SetBool("right_long",true);
            } else {
                transform.Translate(0, 0, 0);
            }
        } else {
            anim.SetBool("is_left", false);
            anim.SetBool("is_right", false);
            anim.SetBool("right_long", false);
            anim.SetBool("left_long", false);
        }
        

        //0.5秒ごとに弾を撃ち続ける
        if (time > 0.5f) {
            Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(shotSE, transform.position);
            time = 0.0f;
        }
        time += Time.deltaTime;
        

    }

    //ダメージを受けた時に点滅してこの間だけレイヤーを変更して
    //衝突しないようにすることで無敵になる
    IEnumerator Blink() {
        this.gameObject.layer = LayerMask.NameToLayer("Invisible");

        for (int i=0;i < 4; i++) {
            renderer.enabled = !renderer.enabled;

            yield return new WaitForSeconds(0.5f);
        }
        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }


    //弾との衝突判定
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemyprojectile") {

            //点滅する
            StartCoroutine("Blink");

            AudioSource.PlayClipAtPoint(deadSE,transform.position);
            if(destroyedCount < 2) {
                destroyedCount += 1;
                UpdateHpIcon();
            }else{
                //残機を3機失ったらゲームオーバー
                SceneManager.LoadScene("GameOverScene");
            }
            
        }
    }
    //ダメージを受けたら残機が減る
    void UpdateHpIcon() {
        for(int i = 0; i < hpIcon.Length ; i++) {
            if(destroyedCount <= i) {
                hpIcon[i].SetActive(true);
            } else {
                hpIcon[i].SetActive(false);
            }
        }
    }



}
