using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable 
{     
    public void Move(Transform movableTranform, Vector2 moveVector, float speed, float delta);
}
