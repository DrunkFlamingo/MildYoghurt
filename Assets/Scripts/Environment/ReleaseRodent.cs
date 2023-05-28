using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReleaseRodent : MonoBehaviour
{
    private bool _isInRange = false;
    [Header("Rodents function to start itself")]
    [SerializeField]private UnityEvent _releaseRodent;

    
    private void Update()
    {
        if (_isInRange)
        {
            if (Input.GetButtonDown("Submit"))
            {
                _releaseRodent.Invoke();
                Destroy(this.GetComponent<Collider2D>());
                Destroy(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }
}
