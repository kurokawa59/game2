using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class BombController : MonoBehaviour
{
    [SerializeField] private GameObject ProjectilePrefab;

    private float time;
    private int BombCount;

    
    void Start()
    {
        time = 0.0f;
    }

    
    void Update()
    {
        BombCount = GameObject.FindGameObjectsWithTag("Bomb").Length;//爆弾の数
        if (time > 1.0f) {
            if (BombCount < 3) {
                explode(3);
                time = 0.0f;
            }
            
        }

        time += Time.deltaTime;
    }

    //爆発して弾をNwayCount方向に飛ばす
    public void explode(int NwayCount) {
        
        for (int i = 1; i <= NwayCount; i++) {
            float angle = -(NwayCount + 1) * 10 / 2 + i * 10;
            Instantiate(ProjectilePrefab, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle)));
        }
        Destroy(gameObject);
        
        
    }
}
