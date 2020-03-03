using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//瞬間移動する敵の制御
public class TeleportEnemyController : SpaceShip
{
    public int count;

    // Update is called once per frame
    void Update() {

        //Time.deltatimeごとに3回素早く敵が動く
        if (count < 2) {
            base.translate2(2, 1);
            count += 1;

        }
        //生成されて1秒後に弾を撃つ
        //1秒後にまた動く
        if (time > 1.0f) {
            base.Shot(gameObject.transform);
            count = 0;
            time = 0.0f;
        }
        
        time += Time.deltaTime;
    }
}
