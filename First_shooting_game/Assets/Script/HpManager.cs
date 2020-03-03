using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ボスの体力バーを操作するスクリプト
public class HpManager : MonoBehaviour
{

    private Slider HpBar;

    
    void Start()
    {
        HpBar = GameObject.Find("BossHpBar").GetComponent<Slider>();
        
    }

    public void SetHp(int hp) {
        HpBar.maxValue = hp;
        HpBar.value = hp;
    }

    public void HpDown(GameObject gameObject) {
        if (HpBar.value > 1) {
            HpBar.value -= 1;
        }else if (HpBar.value == 1) {
            Destroy(gameObject);
        }
        
    }
}
