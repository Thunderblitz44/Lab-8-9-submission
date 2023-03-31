using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  
    
  
    private float nextAttackTime = 0f;
    public float speed = 6.0f;
    public float mouseSensitivity = 2.0f;
    public float upDownRange = 60.0f;
    public float jumpSpeed = 8.0f;
    public float moveSpeed = 4.0f;
    public float jumpCooldown = 1.0f; 
    public float dashSpeed = 10f; 
    public float dashTime = 0.5f;
    public float acceleration = 0.1f; 
    public float deceleration = 0.1f;
    private float currentSpeed; 
    private float dashTimer; 
    private bool isDashing;
    private float dashCoolTimer;
    public float dashCooldown = 1f;
    float verticalRotation = 0;
    float verticalVelocity = 0;
    public float cooldown; 
    public string button; 
    bool doubleJumped = false;
    float lastJumpTime = 0;
    private Vector3 moveDirection;
    CharacterController cc;



    private bool _isInvincible = false;
    public bool IsInvincible
    {
        get { return _isInvincible; }
        set { _isInvincible = value; }
    }
  
    void Start()
    {
      
        
        cc = GetComponent<CharacterController>();
        Cursor.visible = false;
      
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }
    void Update()
    {
        
      
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        float forwardSpeed = Input.GetAxis("Vertical") * 4;

        float sideSpeed = Input.GetAxis("Horizontal") * 4;

        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

        speed = transform.rotation * speed;
        cc.Move(speed * Time.deltaTime); 
        Vector3 velocity = cc.velocity;                  
        if (cc.isGrounded)
        {
           
            doubleJumped = false;
            if (Input.GetButtonDown("Jump"))
            {
               
                verticalVelocity = jumpSpeed;
                lastJumpTime = Time.time;
            
            }
        }
        else
           
        {
            if (Input.GetButtonDown("Jump") && !doubleJumped && Time.time - lastJumpTime >= jumpCooldown)
            {
                doubleJumped = true;
                verticalVelocity = jumpSpeed;
                lastJumpTime = Time.time;
            }
            else
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");      
        if (Input.GetButtonDown("Dash") && !isDashing && dashCoolTimer <= 0)
        {
       
            
            moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= dashSpeed;
            dashTimer = dashTime;
            dashCoolTimer = dashCooldown;
            isDashing = true;
        }
        if (isDashing)
        {
       
            dashTimer -= Time.deltaTime;
            cc.Move(moveDirection * Time.deltaTime);
         
            if (dashTimer <= 0)
            {
              
                isDashing = false;
            }
        }
        else
        {
            moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
           
            if (horizontal != 0 || vertical != 0)
            {
          
                moveSpeed = Mathf.Lerp(moveSpeed, moveSpeed, acceleration * Time.deltaTime);
            }
            else
            {
           
                moveSpeed = Mathf.Lerp(moveSpeed, 0, deceleration * Time.deltaTime);
            }
            cc.Move(moveDirection * Time.deltaTime);
        }     
        if (dashCoolTimer > 0)
        {
            dashCoolTimer -= Time.deltaTime;
        }
        {
      
        }


        if (Input.GetButtonDown("Fire1") && Time.time >= nextAttackTime)
        {
           
            nextAttackTime = Time.time + cooldown;
        }

      
      
    
}



    private IEnumerator InvincibilityCoroutine(float duration)
    {
        IsInvincible = true;
        yield return new WaitForSeconds(duration);
        IsInvincible = false;
    }

    public void StartInvincibility(float duration)
    {
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    //Lab 7
    //invincibility for player

}

