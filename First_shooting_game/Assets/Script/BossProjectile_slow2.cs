using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボスの弾の制御(通常よりは遅い弾)
public class BossProjectile_slow2 : ProjectileController
{
    //オブジェクトが消える座標
    private float xlimit1;
    private float xlimit2;
    private float ylimit1;
    private float ylimit2;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        xlimit1 = -6.0f;
        xlimit2 = 6.0f;
        ylimit1 = -6.0f;
        ylimit2 = 6.0f;

    }

    // Update is called once per frame
    void Update()
    {
        base.projectile(0, -0.08f);
        if (transform.position.y > ylimit2 || transform.position.y < ylimit1 || transform.position.x < xlimit1 || transform.position.x > xlimit2) {
            Destroy(gameObject);
        }
    }
}
