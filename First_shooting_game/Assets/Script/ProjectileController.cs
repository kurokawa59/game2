using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弾の制御の親クラス
public class ProjectileController : MonoBehaviour {
    public float limit;

    public void projectile(float x, float y) {
        transform.Translate(x, y, 0);
    }

}
