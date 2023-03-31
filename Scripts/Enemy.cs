using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float moveSpeed;

    public override void Attack(Character target)
    {
       
        target.TakeDamage(damage * 2);
    }
}