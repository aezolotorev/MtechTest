using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="MTechTestProject/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public float speed = 3.0f;
    public int health = 3;
    public Sprite sprite;
}
