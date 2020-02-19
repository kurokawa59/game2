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
        //1.5秒経つと止まり2.0秒になったら弾を撃つ
        if (time > 1.0f) {
            StartCoroutine(enemytrans_and_shot());
            time = 0.0f;
        } 

        time += Time.deltaTime;
    }

    //移動と通常攻撃のかたまり
    IEnumerator enemytrans_and_shot() {

        base.translate(1.0f, 2.0f);

        yield return new WaitForSeconds(0.5f);

        base.stop();

        yield return new WaitForSeconds(0.1f);

        base.Shot(gameObject.transform);
        
        
    }


}
