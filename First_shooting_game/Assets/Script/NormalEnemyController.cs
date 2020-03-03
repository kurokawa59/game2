using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通常の敵の制御
public class NormalEnemyController : SpaceShip
{
    // Update is called once per frame
    void Update()
    {

        //生成されて1.0秒後に移動
        
        if (time > 1.0f) {
            if (AttackFlag == 0) {
                enemytrans_and_shot();
                time = 0.0f;
                AttackFlag = 1;
            }else if (AttackFlag == 1) {
                base.stop();
                time = 0.0f;
                AttackFlag = 0;
            }
            
        } 

        time += Time.deltaTime;
    }

    //移動と通常攻撃のかたまり
    public void enemytrans_and_shot() {

        base.translate(1.0f, 2.0f);

        base.Shot(gameObject.transform);

    }


}
