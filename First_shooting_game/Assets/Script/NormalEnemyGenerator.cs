using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyGenerator : MonoBehaviour
{
    public float time = 0.0f;
    public GameObject NormalEnemyPrefab;
    public int enemycount;

    // Update is called once per frame
    void Update()
    {
        float x = Random.Range(-3.0f,3.0f);
        //3秒ごとに敵を生成
        if (time > 3.0f) {
            //NormalEnemyオブジェクトの数を数える
            int count = GameObject.FindGameObjectsWithTag("NormalEnemy").Length;
            //敵は5体まで生成
            if (count < enemycount) {
                Instantiate(NormalEnemyPrefab, new Vector3(x, 6.0f, 0.0f), Quaternion.identity);
                time = 0.0f;
            }
            
        }
        
        time += Time.deltaTime;
    }
}
