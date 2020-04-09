using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour
{
    [SerializeField] private GameObject ShieldPrefab;
    private float time;
    private int firstCount;//ボスCが最初に出現したときのシールド


    void Start()
    {
        time = 0.0f;
        firstCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        int BossCCount = GameObject.FindGameObjectsWithTag("BossC").Length;//ボスCの数
        int ShieldCount = GameObject.FindGameObjectsWithTag("Shield").Length;//シールドの数
        if (BossCCount == 1) {
            if (firstCount == 1) {
                Instantiate(ShieldPrefab, new Vector2(-2.0f, 6), Quaternion.identity);
                Instantiate(ShieldPrefab, new Vector2(2.0f, 6), Quaternion.identity);
                firstCount = 0;
            } else {
                if (ShieldCount == 0) {
                    if (time > 60.0f) {
                        Instantiate(ShieldPrefab, new Vector2(-2.0f, 6), Quaternion.identity);
                        Instantiate(ShieldPrefab, new Vector2(2.0f, 6), Quaternion.identity);
                        time = 0.0f;
                    }
                } else if (ShieldCount != 0) {
                    time = 0.0f;
                }
            }
        } else {
            time = 0.0f;
        }
        time += Time.deltaTime;
    }

    

}
