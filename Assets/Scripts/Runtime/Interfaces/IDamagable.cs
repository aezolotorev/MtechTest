using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable  
{
    public void SetDamage(IHealth health, int damage);
}
