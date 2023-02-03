using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using kaputt.Character;
using Unity.Netcode;
using UnityEngine.UI;

namespace kaputt.Ui
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] PlayerHealth playerHealth;
        Slider slider;

        float maxWidth;

        void Start(){
            slider = GetComponent<Slider>();
            maxWidth = GetComponent<RectTransform>().sizeDelta.x;
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerHealth.health.OnValueChanged += handleChange;
        }

        void OnDestroy()
        {
            playerHealth.health.OnValueChanged -= handleChange;
        }


        public void handleChange(int oldHealth, int newHealth){
            slider.value = newHealth;
        }
    }
}
