using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType { left, right, top, bottom }

public class Door : MonoBehaviour
{
    public DoorType doorType;
}
