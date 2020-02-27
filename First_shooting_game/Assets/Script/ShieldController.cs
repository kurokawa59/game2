using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//生成されたシールドの動き
public class ShieldController : MonoBehaviour
{
    private float time;
    private Rigidbody2D rb;
    Slider HpBar;

    [SerializeField]private int Hp;
    [SerializeField] private AudioClip BossDamagedSE;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HpBar = GetComponentInChildren<Slider>();

        rb.velocity = new Vector2(0,-2.0f);

        HpBar.maxValue = Hp;//シールドの体力
        HpBar.value = Hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0.2f) {
            if(transform.position.x < 0) {
                //画面左に生成されたとき
                translate_Left(1.0f, 0.5f);
            } else if (transform.position.x > 0) {
                //画面右に生成されたとき
                translate_Right(1.0f, 0.5f);
            }

            time = 0.0f;
        }
        time += Time.deltaTime;
    }

    //画面左のシールドの動き
    public void translate_Left(float rand_x, float rand_y) {
        float x = Random.Range(-rand_x, rand_x);
        float y = Random.Range(-rand_y, rand_y);
        float x_pos = transform.position.x;
        float y_pos = transform.position.y;
        //x軸方向の条件
        if (x_pos + x > -3.0f && x_pos + x < 0) {
            if (y_pos + y < 4.4f && y_pos + y > 3.0f) {
                rb.velocity = new Vector2(x, y);
            }
        }
    }

    //画面右のシールドの動き
    public void translate_Right(float rand_x, float rand_y) {
        float x = Random.Range(-rand_x, rand_x);
        float y = Random.Range(-rand_y, rand_y);
        float x_pos = transform.position.x;
        float y_pos = transform.position.y;
        //x軸方向の条件
        if (x_pos + x > 0 && x_pos + x < 3.0f) {
            if (y_pos + y < 4.4f && y_pos + y > 3.0f) {
                rb.velocity = new Vector2(x, y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        float y_pos = transform.position.y;
        if (y_pos < 5.4) {
            if (collision.gameObject.tag == "Playerprojectile") {
                AudioSource.PlayClipAtPoint(BossDamagedSE, transform.position);
                if (HpBar.value > 1) {
                    HpBar.value -= 1;
                } else if (HpBar.value == 1) {
                    Destroy(gameObject);
                }
                Destroy(collision.gameObject);
            }
        }
    }
}
