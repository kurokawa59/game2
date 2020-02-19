using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelepoEnemyGenerator : MonoBehaviour
{
    protected float time;
    public GameObject TelepoEnemyPrefab;
    public int enemycount;

    // Update is called once per frame
    void Update() {
        float x = Random.Range(-3.0f, 3.0f);
        //3秒ごとに敵を生成
        if (time > 3.0f) {
            int count = GameObject.FindGameObjectsWithTag("TelepoEnemy").Length;
            //敵は3体まで生成
            if (count < enemycount) {
                Instantiate(TelepoEnemyPrefab, new Vector3(x, 6.0f, 0.0f), Quaternion.identity);
                time = 0.0f;
                
            }
        }

        time += Time.deltaTime;
    }
}
