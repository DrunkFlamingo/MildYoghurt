using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMOD.Studio;

namespace Cat.UI {
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        [Header("UI Elements")]
        [SerializeField] private GameObject lockdownTimer;

        private Cat.LevelManager levelManager;
        private TextMeshProUGUI lockdownTimerText;
        //Audio
        private EventInstance lockdownTimerSound;


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
            //Audio
            lockdownTimerSound = AudioManager.instance.CreateEventInstance(FMODEvents.instance.lockdownTimerSound);

        }
        void OnGUI() {
            if (levelManager.lockdownTriggered) {
                lockdownTimer.SetActive(true);
                lockdownTimerText.text = ((double)Mathf.Max(levelManager.lockdownTimer, 0)).ToString("0.00");
                //Audio start playing
                lockdownTimerSound.start();
            }
        }
    }
}