using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);
        if(transform.position.y <= -20.44f) {
            transform.position = new Vector3(0, 18.44f, 0);
        }
        
    }
}
