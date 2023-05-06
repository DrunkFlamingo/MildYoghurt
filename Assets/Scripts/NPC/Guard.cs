using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cat.Player;

namespace Cat.NPC {
    public class Guard : MonoBehaviour
    {

        [Header("Detection")]
        [SerializeField] private Collider2D detectionCone;
        [SerializeField] private GameObject detectionIndicator;
        [Header("Movement")]
        [SerializeField] private float patrolSpeed = 4f;
        [SerializeField] private float alertedMoveSpeed = 4f;
        [SerializeField] private Transform patrolPointA;
        [SerializeField] private Transform patrolPointB;

        private ContactFilter2D contactFilter;
        private Rigidbody2D rb;

        private int patrolDirection = 0;
        private Transform currentPatrolPoint;

        public bool detectedPlayer = false;


        void Start() {
            // Set up contact filter
            contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Player"));
            contactFilter.useLayerMask = true;
            // reference to own rigidbody
            rb = GetComponent<Rigidbody2D>();
        }


        void GuardDetectsPlayer() {
            detectedPlayer = true;
            detectionIndicator.SetActive(true);
            GameObject[] lockdownButtons = GameObject.FindGameObjectsWithTag("LockdownButton");
            GameObject closestButton = null;
            float closestDistance = 10000f;
            foreach (GameObject button in lockdownButtons) {
                float distance = Mathf.Abs(button.transform.position.x - transform.position.x);
                if (distance < closestDistance) {
                    closestButton = button;
                    closestDistance = distance;
                }
            }
            currentPatrolPoint = closestButton.transform;
            patrolDirection = (int)Mathf.Sign(currentPatrolPoint.position.x - transform.position.x);
            transform.localScale = new Vector3(patrolDirection, 2.5f, 1);
            rb.velocity = new Vector2(patrolDirection * alertedMoveSpeed, rb.velocity.y);
        }



        void PatrolLogic() {
            if (currentPatrolPoint == null) {
                currentPatrolPoint = patrolPointA;
            }
            if (patrolDirection == 0) {
                patrolDirection = (int)Mathf.Sign(currentPatrolPoint.position.x - transform.position.x);
            }
            if (Mathf.Abs(currentPatrolPoint.position.x - transform.position.x) < 0.1f) {
                if (currentPatrolPoint == patrolPointA) {
                    currentPatrolPoint = patrolPointB;
                } else {
                    currentPatrolPoint = patrolPointA;
                }
                patrolDirection = (int)Mathf.Sign(currentPatrolPoint.position.x - transform.position.x);
                transform.localScale = new Vector3(patrolDirection, 2.5f, 1);
            }
        }


        void Update() {
            if (detectedPlayer) {
                //check for point overlap with LockdownButton layer
                ContactFilter2D lockdownFilter = new ContactFilter2D();
                lockdownFilter.SetLayerMask(LayerMask.GetMask("LockdownButton"));
                lockdownFilter.useLayerMask = true;
                lockdownFilter.useTriggers = true;
                Collider2D[] lockdownTriggerResults = new Collider2D[1];
                int numLockdownTriggerResults = Physics2D.OverlapPoint(transform.position, lockdownFilter, lockdownTriggerResults);
                if (numLockdownTriggerResults > 0) {
                    Cat.LevelManager.instance.TriggerLockdown();
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                return;
            }

            // Check if player is in detection cone
            Collider2D[] results = new Collider2D[1];
            int numResults = detectionCone.OverlapCollider(contactFilter, results);
            if (numResults > 0) {
                GameObject player = results[0].gameObject;
                if (player.GetComponent<Detectable>().isDetectable) {
                    GuardDetectsPlayer();
                    return;
                }
            }
            if (patrolPointA != null && patrolPointB != null) {
                PatrolLogic();
            }
        }
    }
}