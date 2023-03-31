using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    public int damage;

    public virtual void Attack(Character target)
    {
       
        target.TakeDamage(damage);
    }

    public virtual void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(characterName + " has died!");
        Destroy(gameObject);
    }
}
