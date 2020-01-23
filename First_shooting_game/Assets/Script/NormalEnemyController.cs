using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通常の敵の制御
public class NormalEnemyController : SpaceShip
{
    // Update is called once per frame
    void Update()
    {

        //生成されて1秒後に弾を撃つ
        //2秒ごとに敵が動く
        if (time > 2.5f && time < 3.0f) {
            base.translate(0.01f, 0.02f);
        }else if (time > 3.0f) {
            base.Shot(gameObject.transform);
            time = 0.0f;
        }

        time += Time.deltaTime;
    }



}
