using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cat {
    public class LevelManager : MonoBehaviour
    {
        [Header("Level Objectives")]
        [SerializeField] private int numCatsRequired = 3;

        [Header("Lockdown")]
        [SerializeField] private GameObject[] lockdownDoors; //used for triggering visual change in doors before transitioning to game over
        [SerializeField] private float lockdownTimerDuration = 15f; //timer after lockdown trigger to escape the level.

        public static LevelManager instance;

        public bool lockdownTriggered {get; private set;} = false;
        public float lockdownTimer {get; private set;} = 0f;

        public void TriggerLockdown() {
            if (lockdownTriggered) return;
            lockdownTriggered = true;
            lockdownTimer = lockdownTimerDuration;
        }

        void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(instance.gameObject);
                instance = this;
            }
        }

        void Update() {
            if (lockdownTriggered) {
                lockdownTimer -= Time.deltaTime;
                if (lockdownTimer <= 0f) {
                    //Game Over
                }
            }
        }

        
    }
}