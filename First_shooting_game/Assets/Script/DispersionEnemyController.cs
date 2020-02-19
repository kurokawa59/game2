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
            StartCoroutine("enemytrans_and_shot");
            
            time = 0.0f;
        }

        time += Time.deltaTime;
    }

    IEnumerator enemytrans_and_shot() {

        base.translate(1.0f, 2.0f);

        yield return new WaitForSeconds(0.5f);

        base.stop();

        yield return new WaitForSeconds(0.1f);

        for (int i = 1; i <= NwayCount; i++) {
            float angle = -(NwayCount + 1) * NwayAngle / 2 + i * NwayAngle;
            base.NwayShot(gameObject.transform, angle);
        }
        
    }
}
