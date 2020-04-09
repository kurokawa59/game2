using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ラスボスが出す爆弾
public class BombController2 : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;

    private float time;

    void Start() {
        time = 0.0f;
    }


    void Update() {
        int bombC = GameObject.FindGameObjectsWithTag("Bomb").Length;
        if (time > 1.0f) {
            if(bombC >= 4) {
                StartCoroutine(explode(5));
            }
            time = 0.0f;

        }

        time += Time.deltaTime;
    }

    //爆発するとNwayCount方向に弾が飛ぶ
    IEnumerator explode(int NwayCount) {
        yield return new WaitForSeconds(1);

        for (int i = 1; i <= NwayCount; i++) {
            float angle = -(NwayCount + 1) * 10 / 2 + i * 10;
            Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
        }

        Destroy(gameObject);
    }
}
