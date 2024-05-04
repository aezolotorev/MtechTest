using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : IInput
{
    public Vector2 Move
    {
        get { return move; }
        set { move = value; }
    }
    public Vector2 move;
    public bool attack { get; set; } 
    
    public void TickUpdate()
    {        
        move = (Input.GetAxis("Horizontal") * Vector2.right + Input.GetAxis("Vertical") * Vector2.up).normalized;
        Debug.Log(move);
    }
}
