using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodentTemp : MonoBehaviour
{
    [SerializeField]private GameObject switchBoxObject;
    private Transform _switchBoxTransform;
    private Collider _switchBoxCollider;
    private Transform _myTransform;

    [SerializeField] private float speed = 1;
    private bool _isFreed;
    // Start is called before the first frame update
    void Start()
    {
        _isFreed = false;
        _myTransform = transform;
        _switchBoxTransform = switchBoxObject.transform;
        _switchBoxCollider = switchBoxObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isFreed = true;
        }
        if (_isFreed == true)
        {
            if (_switchBoxTransform.position.x >= 0)
            {
                _myTransform.position = new Vector3(_myTransform.position.x + (speed * Time.deltaTime),
                    _myTransform.position.y, _myTransform.position.z);
            }
            else if(_switchBoxTransform.position.x < 0)
            {
                _myTransform.position = new Vector3(_myTransform.position.x + (speed * Time.deltaTime *(-1)),
                    _myTransform.position.y, _myTransform.position.z);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.collider == _switchBoxCollider)
        {
            _isFreed = true;
            Debug.Log("Hit SwitchBox");
        }
        if (collision.gameObject.CompareTag("SwitchBox"))
        {
            _isFreed = true;
            Debug.Log("Hit SwitchBox");
        }
    }
}
