using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通常の敵の制御
public class AvatarController : MonoBehaviour
{
    private float time;
    [SerializeField]private GameObject BossProjectilePrefab;
    [SerializeField]private AudioClip enemyshotSE;
    [SerializeField] private AudioClip enemydeadSE;

    private PlayerController player;

    void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        //生成されて0.3秒後に移動
        if (time > 1.0f) {
            Shot(gameObject.transform);
            time = 0.0f;
        } 

        time += Time.deltaTime;
    }

    public void Shot(Transform enemy) {
        Instantiate(BossProjectilePrefab, enemy.position, enemy.rotation);
        AudioSource.PlayClipAtPoint(enemyshotSE, transform.position);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        float y_pos = transform.position.y;
        //プレイヤーとの当たり判定
        if (collision.gameObject.tag == "Player") {
            player.destroyedCount += 1;
        }

        //弾が当たったら敵と弾が消える
        if (y_pos < 5.4) {
            if (collision.gameObject.tag == "Playerprojectile") {
                AudioSource.PlayClipAtPoint(enemydeadSE, transform.position);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }

    }

}
