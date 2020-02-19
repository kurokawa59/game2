using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private Text Scorelabel;
    public int clearlimit;
    public AudioClip clearSE;
    private AudioSource source;

    
    void Start()
    {
        //オブジェクトのスコアラベルのテキストを取得してこれを更新する
        Scorelabel = GameObject.Find("Scorelabel").GetComponent<Text>();
        Scorelabel.text = "Score:" + score;
        source = GetComponent<AudioSource>();
    }
    //目標スコアにいったらゲームクリア
    void Update() {
        if (score == clearlimit) {
            SceneManager.LoadScene("GameClearScene");
            source.PlayOneShot(clearSE);
        }
    }

    //敵オブジェクトが消滅したときのポイント加算の関数
    public void AddScore(int count) {
        score += count;
        Scorelabel.text = "Score:" + score;
    }
}
