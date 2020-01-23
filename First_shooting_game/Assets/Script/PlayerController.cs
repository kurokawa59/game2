using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//プレイヤーの制御
public class PlayerController : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float time = 0.0f;
    public int destroyedCount = 0;//倒された数
    public static PlayerController Player_Instance;
    public AudioClip deadSE;
    public AudioClip shotSE;

    public GameObject[] hpIcon;

    void Start() {
        Player_Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        //プレイヤーの動き(-3.1 < x < 3.1の範囲で動く)
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if(-3.1 < transform.position.x) {
                transform.Translate(-0.05f, 0, 0);
            } else{
                transform.Translate(0, 0, 0);
            }
        }else if (Input.GetKey(KeyCode.RightArrow)) {
            if (3.1 > transform.position.x) {
                transform.Translate(0.05f, 0, 0);
            } else {
                transform.Translate(0, 0, 0);
            }
        }

        //0.5秒ごとに弾を撃ち続ける
        if (time > 0.5f) {
            Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(shotSE, transform.position);
            time = 0.0f;
        }
        time += Time.deltaTime;
        

    }
    //弾との衝突判定
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemyprojectile") {
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
