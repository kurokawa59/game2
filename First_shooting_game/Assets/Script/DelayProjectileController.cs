using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//追跡する弾のスクリプト
public class DelayProjectileController : ProjectileController
{
    private GameObject player;
    private Rigidbody2D rb;
    private float time;
    private int count;

    //オブジェクトが消える座標
    private float xlimit1;
    private float xlimit2;
    private float ylimit1;
    private float ylimit2;


    void Start() {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        count = 1;

        xlimit1 = -6.0f;
        xlimit2 = 6.0f;
        ylimit1 = -6.0f;
        ylimit2 = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float AngleOffset = 90;
        float speed = 3.0f;
        if (time > 1.0f) {
            if (count == 1) {
                //プレイヤーまでの方向ベクトルを取得
                Vector3 dir = player.transform.position - transform.position;
                //角度を計算
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //angleoffsetで角度を調整
                Vector3 euler = new Vector3(0, 0, angle + AngleOffset);
                //Quaternionに変換
                transform.rotation = Quaternion.Euler(euler);
                rb.velocity = dir.normalized * speed;
                count = 0;
            }

        }
        time += Time.deltaTime;

        if (transform.position.y > ylimit2 || transform.position.y < ylimit1 || transform.position.x < xlimit1 || transform.position.x > xlimit2) {
            Destroy(gameObject);
        }
    }
}
