using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RodentTemp : MonoBehaviour
{
    [SerializeField]private GameObject switchBoxObject;
    [SerializeField] private GameObject lightObject;
    private Transform _switchBoxTransform;
    private Rigidbody2D _myRigidbody;

    [SerializeField] private float speed = 1;
    private bool _isFreed;
    // Start is called before the first frame update
    void Start()
    {
        _isFreed = false;
        _switchBoxTransform = switchBoxObject.transform;
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isFreed = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isFreed == true)
        {
            if (_switchBoxTransform.position.x >= _myRigidbody.position.x)
            {
                _myRigidbody.position = new Vector2(_myRigidbody.position.x + (speed * Time.fixedDeltaTime),
                    _myRigidbody.position.y);
            }
            else if(_switchBoxTransform.position.x < _myRigidbody.position.x)
            {
                _myRigidbody.position = new Vector2(_myRigidbody.position.x - (speed * Time.fixedDeltaTime),
                    _myRigidbody.position.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SwitchBox"))
        {
            _isFreed = false;
            Destroy(col);
            Destroy(lightObject.GetComponent<Collider2D>());
            lightObject.GetComponent<Light2D>().intensity = 0.1f;
        }
    }
}
