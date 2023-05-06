using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectable : MonoBehaviour
{
    private List<Collider2D> overlappedLights = new List<Collider2D>();
    private ContactFilter2D contactFilter;
    private Collider2D ownCollider;

    public bool isDetectable {get; private set;} = false;
    
    void Start() {
        // Set up contact filter
        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("StealthSystemLight"));
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = true;
        // reference to own collider
        ownCollider = GetComponent<Collider2D>();
    }
    void Update()
    {
        overlappedLights.Clear();
        Physics2D.OverlapCollider(ownCollider, contactFilter, overlappedLights);
        
        isDetectable = overlappedLights.Count > 0;
    }
}
