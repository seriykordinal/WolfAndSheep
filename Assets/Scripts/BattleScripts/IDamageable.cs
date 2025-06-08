using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{

    public int HungerForKill { get; }
    public int ScoreForKill { get; }
    public int Health { get; }
    void TakeDamage(int damage);
    void TakeHeal(int heal);
    void Stun(float sec);

}
