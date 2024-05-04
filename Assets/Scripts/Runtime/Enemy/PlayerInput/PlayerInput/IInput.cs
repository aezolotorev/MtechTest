using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    public Vector2 Move { get; set; }       
    bool attack{ get; set; }   
    public void TickUpdate();
}
   
