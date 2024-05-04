using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MTechTestProject/PlayerBulletConfig")]
public class BulletConfig : ScriptableObject
{
    public BulletData commonBulletData;
    public BulletData rareBulletData;
    public BulletData epicBulletData;
    public BulletData legendaryBulletData;

}
