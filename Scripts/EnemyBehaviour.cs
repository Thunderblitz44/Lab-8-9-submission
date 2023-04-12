using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float followDistance = 3f; 
    public CharacterController controller; 
    public float speed = 2f; 
    public float gravity = 9.81f; 
    public float maxHealth = 100f; 
    private float currentHealth;
    private Vector3 velocity; 
    private Transform player; 
    void Start()
    {   
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }
    void Update()
    {       
        if (player != null)
        {         
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > followDistance && currentHealth > 0)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                velocity.y -= gravity * Time.deltaTime;
                controller.Move(direction * speed * Time.deltaTime + velocity * Time.deltaTime);
            }
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
