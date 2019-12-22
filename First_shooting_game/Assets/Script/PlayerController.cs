using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの制御
public class PlayerController : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public float time = 0.0f;
    public static PlayerController Player_Instance;

    void Start() {
        Player_Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        //プレイヤーの動き(-3.1 < x < 3.1の範囲で動く)
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if(-3.1 < transform.position.x) {
                transform.Translate(-0.05f, 0, 0);
            } else{
                transform.Translate(0, 0, 0);
            }
        }else if (Input.GetKey(KeyCode.RightArrow)) {
            if (3.1 > transform.position.x) {
                transform.Translate(0.05f, 0, 0);
            } else {
                transform.Translate(0, 0, 0);
            }
        }

        if (time > 0.5f) {
            Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
            time = 0.0f;
        }
        time += Time.deltaTime;
        

    }

    
    
}
