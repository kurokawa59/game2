using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBarController : MonoBehaviour
{
    Slider slider;
    public float CurrentHp;
    public float MaxHp;

    void Start()
    {
        slider = GameObject.Find("BossHpBar").GetComponent<Slider>();
        slider.maxValue = MaxHp;
        slider.value = CurrentHp;
    }

}
