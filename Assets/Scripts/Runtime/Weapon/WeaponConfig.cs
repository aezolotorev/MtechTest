using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MTechTestProject/WeaponConfig")]
public class WeaponConfig : ScriptableObject {
    public List<WeaponData> weapons;
}
