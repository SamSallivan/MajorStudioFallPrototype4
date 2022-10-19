using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Data
[System.Serializable]
public class Model 
{
    // Set in editor
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    // Movement
    public Vector3 XMove { get; set; }
    public Vector3 YMove { get; set; }    
    public bool Jump { get; set; }
    public Vector3 _moveDirection = Vector3.zero;
    public Vector3 MoveDirection
    {
        get { return _moveDirection; }
        set { _moveDirection = value; }
    }


    // Set in editor
    public float sensitivity = 100f;

    // Movement
    public float MouseX { get; set; }
    public float MouseY { get; set; }
    public bool Shift { get; set; }
    public float horizontalRotation;// { get; set; }
    public float verticalRotation;// { get; set; }

}
