using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private AudioSource audioSource;

    //変数類
    private int BossBCount;
    private int BossCCount;
    private int BossDCount;
    private int LastBossCount;

    [SerializeField] protected AudioClip BossDeadSE;

    void Start() {

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioClip1;
        audioSource.Play();

        Instantiate(BossAPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
        BossBCount = 1;
        BossCCount = 0;
        BossDCount = 0;
        LastBossCount = 0;
}

    void Update()
    {
        int countA = GameObject.FindGameObjectsWithTag("BossA").Length;//ボスAの数
        int countB = GameObject.FindGameObjectsWithTag("BossB").Length;//ボスBの数
        int countC = GameObject.FindGameObjectsWithTag("BossC").Length;//ボスCの数
        int countD = GameObject.FindGameObjectsWithTag("BossD").Length;//ボスDの数
        int countLast = GameObject.FindGameObjectsWithTag("LastBoss").Length;//ラスボスの数
        int end = 0;

        if (BossBCount == 1) {
            if (countA == 0) {
                audioSource.clip = AudioClip2;
                audioSource.Play();
                Instantiate(BossBPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
                BossBCount = 0;
                BossCCount = 1;
            }
        }else if (BossCCount == 1) {
            if(countB == 0) {
                audioSource.clip = AudioClip3;
                audioSource.Play();
                Instantiate(BossCPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
                BossCCount = 0;
                BossDCount = 1;
            }
        } else if (BossDCount == 1) {
            if (countC == 0) {
                audioSource.clip = AudioClip4;
                audioSource.Play();
                Instantiate(BossDPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
                BossDCount = 0;
                LastBossCount = 1;
            }
        } else if (LastBossCount == 1) {
            if (countD == 0) {
                audioSource.clip = AudioClip5;
                audioSource.Play();
                Instantiate(LastBossPrefab, new Vector3(0.0f, 6.0f, 0.0f), Quaternion.identity);
                LastBossCount = 0;
                end = 1;
            }
        }else if (end == 1) {
            if (countLast == 0) {
                SceneManager.LoadScene("GameClearScene");
            }
        }

    }
}
