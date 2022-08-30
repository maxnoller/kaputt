using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace kaputt.Dummy{
public class DummyHealthBar : MonoBehaviour
{
    [SerializeField]Slider slider;
    [SerializeField]DummyHealth dummyHealth;
    [SerializeField]Transform player;

    void Awake(){
        slider.maxValue = DummyHealth.maxHealth;
        dummyHealth.health.OnValueChanged += updateHealthBar;
    }

    void updateHealthBar(int previousValue, int newValue){
        slider.value = newValue;

    }

    void Update(){
        if(player != null){
            slider.transform.parent.LookAt(player);
        } else {
            player = Camera.main.transform.parent;
        }
    }
}
}