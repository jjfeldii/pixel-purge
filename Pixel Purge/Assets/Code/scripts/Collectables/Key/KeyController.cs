using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyController : MonoBehaviour
{
    public int keyCount { get; set; }

    public UnityEvent OnKeyCountChanged;

    public void AddKey()
    {
        keyCount++;
        OnKeyCountChanged.Invoke();
    }
}
