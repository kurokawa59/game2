using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//ボスを出現させるスクリプト
public class BossGenerator : MonoBehaviour
{
    //ボスのプレファブ
    public GameObject BossAPrefab;
    public GameObject BossBPrefab;
    public GameObject BossCPrefab;
    public GameObject BossDPrefab;
    public GameObject LastBossPrefab;

    //サウンド
    public AudioClip AudioClip1;
    public AudioClip AudioClip2;
    public AudioClip AudioClip3;
    public AudioClip AudioClip4;
    public AudioClip AudioClip5;
    public AudioClip Alart;
    private AudioSource audioSource;


    //変数類
    private int BossBCount;
    private int BossCCount;
    private int BossDCount;
    private int LastBossCount1;
    private int LastBossCount2;

    private int end;

    [SerializeField] protected AudioClip BossDeadSE;
    private Image img;
    private PlayerController Player;

    void Start() {

        audioSource = GetComponent<AudioSource>();
        img = GameObject.Find("Image").GetComponent<Image>();
        img.transform.gameObject.SetActive(false);
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        audioSource.clip = AudioClip1;
        audioSource.Play();

        Instantiate(BossAPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
        BossBCount = 1;
        BossCCount = 0;
        BossDCount = 0;
        LastBossCount1 = 0;
        LastBossCount2 = 0;
        end = 0;
        img.color = Color.clear;
        
        
}

    void Update()
    {
        int countA = GameObject.FindGameObjectsWithTag("BossA").Length;//ボスAの数
        int countB = GameObject.FindGameObjectsWithTag("BossB").Length;//ボスBの数
        int countC = GameObject.FindGameObjectsWithTag("BossC").Length;//ボスCの数
        int countD = GameObject.FindGameObjectsWithTag("BossD").Length;//ボスDの数
        int countLast = GameObject.FindGameObjectsWithTag("LastBoss").Length;//ラスボスの数
        

        if (BossBCount == 1) {
            //ボスAが倒されたら
            if (countA == 0) {
                audioSource.clip = AudioClip2;
                audioSource.Play();
                Instantiate(BossBPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
                BossBCount = 0;
                BossCCount = 1;
            }
        }else if (BossCCount == 1) {
            //ボスBが倒されたら
            if(countB == 0) {
                audioSource.clip = AudioClip3;
                audioSource.Play();
                Instantiate(BossCPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
                BossCCount = 0;
                BossDCount = 1;
            }
        } else if (BossDCount == 1) {
            //ボスCが倒されたら
            if (countC == 0) {
                audioSource.clip = AudioClip4;
                audioSource.Play();
                Instantiate(BossDPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
                BossDCount = 0;
                LastBossCount1 = 1;
            }
        } else if (LastBossCount1 == 1) {
            //ボスDが倒されたらアラートを鳴らす
            if (countD == 0) {
                img.transform.gameObject.SetActive(true);
                for (int i = 0; i < 5; i++) {
                    Player.hpIcon[i].SetActive(true);
                }
                StartCoroutine(Alert(3));
                LastBossCount1 = 0;
            }
        } else if (LastBossCount2==1) {
            //ラスボスを出現
            if (countD == 0) {
                audioSource.clip = AudioClip5;
                audioSource.Play();
                Instantiate(LastBossPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
                LastBossCount2 = 0;
                end = 1;
            }
        } else if (end == 1) {
            if (countLast == 0) {
                SceneManager.LoadScene("GameClearScene");
            }
        }
        

    }

    //ラスボス前にアラート鳴らす
    IEnumerator Alert(int alertcount) {
        audioSource.clip = Alart;
        audioSource.Play();
        for (int i = 0; i < alertcount; i++) {
            float imgcount = 0.0f;
            img.color = new Color(0.5f, 0f, 0f,0.5f);
            for (int j = 0; j < 100; j++) {
                img.color = Color.Lerp(img.color, Color.clear, imgcount);
                imgcount += 0.01f;
                if (i == 0) {
                    yield return new WaitForSeconds(0.01f);
                }else if (i == 1) {
                    yield return new WaitForSeconds(0.02f);
                } else {
                    yield return new WaitForSeconds(0.03f);
                }
                
            }
           
        }


        LastBossCount2 = 1;
    }
}
