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

        private bool lockdownTriggered = false;
        private float lockdownTimer = 0f;


        void Update() {
            if (lockdownTriggered) {
                lockdownTimer += Time.deltaTime;
                if (lockdownTimer >= lockdownTimerDuration) {
                    //Game Over
                }
            }
        }

        
    }
}