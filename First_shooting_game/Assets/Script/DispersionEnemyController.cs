using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispersionEnemyController : SpaceShip
{
    public int NwayCount = 3;//何方向に攻撃するか
    public int NwayAngle = 10;//弾の角度

    // Update is called once per frame
    void Update()
    {
        //生成されて1秒後に弾を撃つ
        //2秒ごとに敵が動く
        if (time > 2.0f) {
            if (AttackFlag == 0) {
                enemytrans_and_shot();
                time = 0.0f;
                AttackFlag = 1;
            } else if (AttackFlag == 1) {
                base.stop();
                time = 0.0f;
                AttackFlag = 0;
            }

        }

        time += Time.deltaTime;
    }



    public void enemytrans_and_shot() {

        base.translate(1.0f, 2.0f);

        for (int i = 1; i <= NwayCount; i++) {
            float angle = -(NwayCount + 1) * NwayAngle / 2 + i * NwayAngle;
            base.NwayShot(gameObject.transform, angle);
        }
        
    }
}
