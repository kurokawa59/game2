using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int score=0;
    private Text Scorelabel;
    public int clearlimit;
    public AudioClip clearSE;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        Scorelabel = GameObject.Find("Scorelabel").GetComponent<Text>();
        Scorelabel.text = "Score:" + score;
        source = GetComponent<AudioSource>();
    }
    void Update() {
        if (score == clearlimit) {
            SceneManager.LoadScene("GameClearScene");
            source.PlayOneShot(clearSE);
        }
    }

    public void AddScore(int count) {
        score += count;
        Scorelabel.text = "Score:" + score;
    }
}
