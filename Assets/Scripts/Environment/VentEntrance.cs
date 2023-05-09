using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentEntrance : MonoBehaviour
{
    
    


    bool VentEnterKeyPressed() {
        return Input.GetKeyDown(KeyCode.E);
    }
}
