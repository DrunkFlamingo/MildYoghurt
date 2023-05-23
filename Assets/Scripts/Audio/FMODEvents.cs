using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    // FMOD Events
    // SFX Events

    [field: Header("Lockdown Timer")]
    [field: SerializeField] public EventReference lockdownTimerSound { get; private set; }

    [field: Header("Panick Button Hit")]
    [field: SerializeField] public EventReference panickButtonSound { get; private set; }

    [field: Header("Guard detection alert")]
    [field: SerializeField] public EventReference guardDetectionSound { get; private set; }



    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene!");
        }
        instance = this;
    }
}
