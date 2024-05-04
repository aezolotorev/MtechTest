using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MTechTestProject/WeaponData")]
public class WeaponData : ScriptableObject
{
   public float weaponDelay;
   public float countOfBarrels;

   public WeaponType weaponType;
}

public enum WeaponType
{
   Common, Rare, Epic, Legendary
}

