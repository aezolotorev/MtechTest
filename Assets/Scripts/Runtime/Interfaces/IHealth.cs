using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public void TakeDamage(int damage);
    public void Die();
    public void AddHealth(int count);


}
