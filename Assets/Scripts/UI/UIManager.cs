using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Cat.UI {
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [Header("UI Elements")]
        [SerializeField] private GameObject lockdownTimer;

        private Cat.LevelManager levelManager;
        private TextMeshProUGUI lockdownTimerText;

        void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(instance.gameObject);
                instance = this;
            }
            lockdownTimerText = lockdownTimer.GetComponent<TextMeshProUGUI>();
        }

        void Start() {
            levelManager = Cat.LevelManager.instance;
        }
        void OnGUI() {
            if (levelManager.lockdownTriggered) {
                lockdownTimer.SetActive(true);
                lockdownTimerText.text = ((double)Mathf.Max(levelManager.lockdownTimer, 0)).ToString("0.00");
            }
        }
        
    }
}