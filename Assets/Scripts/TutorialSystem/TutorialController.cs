using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public static TutorialController Instance { get; private set; }
    
    private Queue<GameObject> _queue;

    private void Start()
    {
        Instance = this;
    }

    public void AddToQueue(GameObject addToQueue)
    {
        _queue.Enqueue(addToQueue);
    }
    
}
