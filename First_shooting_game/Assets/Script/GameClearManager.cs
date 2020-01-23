using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearManager : MonoBehaviour
{
    public void GameClear() {
        SceneManager.LoadScene("TitleScene");
    }
}
