using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispersionEnemyGenerator : MonoBehaviour
{
    protected float time;
    public GameObject DispersionEnemyPrefab;
    public int enemycount;


    // Update is called once per frame
    void Update()
    {
        float x = Random.Range(-3.0f, 3.0f);
        //2秒ごとに敵を生成
        if (time > 2.0f) {
            //NormalEnemyオブジェクトの数を数える
            int count = GameObject.FindGameObjectsWithTag("DispersionEnemy").Length;
            //敵は2体まで生成
            if (count < enemycount) {
                Instantiate(DispersionEnemyPrefab, new Vector3(x, 6.0f, 0.0f), Quaternion.identity);
                time = 0.0f;
            }

        }
        time += Time.deltaTime;
    }
}
