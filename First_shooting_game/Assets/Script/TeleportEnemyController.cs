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
        if (count < 3) {
            base.translate(2.0f, 0.1f);
            count += 1;

        }
        //生成されて1秒後に弾を撃つ
        //1秒後にまた動く
        if (time > 0.5f) {
            base.Shot(gameObject.transform);
            count = 0;
            time = 0.0f;
        }
        
        time += Time.deltaTime;
    }
}
