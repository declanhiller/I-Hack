using UnityEngine;

namespace HackingSystem
{
    public abstract class Usable : MonoBehaviour
    {
        public abstract void Use(bool value);
        
        public bool IsOpen { get; protected set; }
    }
}