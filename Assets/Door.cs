using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    public bool IsOpen { get; private set; }
    
    public void UseDoor(bool open) {
        if (open) {
            Debug.Log("Open Door");
        }
        else {
            Debug.Log("Close Door");
        }

        IsOpen = open;
    }
}
