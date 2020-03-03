using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : MonoBehaviour
{
    
    private float limit;
    private GameObject player;
    private Rigidbody2D rb;
    private float time;

    void Start() {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        limit = -6.0f;
        time = 0.0f;
    }
    // Update is called once per frame
    void Update(){
        if(time < 0.5f) {
            Track(gameObject.transform);
        }
        if (transform.position.y < limit) {
            Destroy(gameObject);
        }
        time += Time.deltaTime;

    }
    public void Track(Transform t) {
        Vector2 vec = player.transform.position - t.position;
        rb.velocity = vec.normalized*6;//正規化したものを6倍
    }

}
