using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public void NormalGame() {
        SceneManager.LoadScene("NormalScene");
    }

    public void HardGame() {
        SceneManager.LoadScene("HardScene");
    }

    public void BossGame() {
        SceneManager.LoadScene("ExtremeScene");
    }
}
