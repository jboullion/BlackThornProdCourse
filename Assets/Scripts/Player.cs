using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    // [SerializeField] float climbSpeed = 3f;
    // [SerializeField] float jumpForce = 5f;
    // [SerializeField] float deathKick = 5f;
   

    // State
    bool isAlive = true;
    bool turning = false;
    Vector2 velocity;

    // GameObjects
    Rigidbody2D playerRigidBody2D;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider2D;
    BoxCollider2D playerFeetCollider2D;


    void Awake()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider2D = GetComponent<CapsuleCollider2D>();
        playerFeetCollider2D = GetComponent<BoxCollider2D>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame (ie: 120fps)
    void Update()
    {

        if (isAlive)
        {
            GetMovement();
        }

        //CheckEnemies();

    }

    // Update anything with Physics simulations, like RigidBodies (50/second)
    private void FixedUpdate()
    {
        FixedMove();
    }

    /**
     * Get all the movements and actions of this character. 
     */
    private void GetMovement()
    {
        // IF we don't have any carry over velocity we can stop running
        if (velocity == Vector2.zero)
        {
            playerAnimator.SetBool("isRunning", false);

        }
        else
        {
            FlipSprite();
        }

        // Directional Inputs
        float h_movement = CrossPlatformInputManager.GetAxis("Horizontal");
        float v_movement = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(h_movement, v_movement);
  
        // Normalize so diagonal movements do not increase speed
        velocity = moveInput.normalized * runSpeed;

        // If moving start running, if not moving stop movement and animations
        if(moveInput != Vector2.zero)
        {
            playerAnimator.SetBool("isRunning", true);
            
        }


    }

    // Move the Player
    private void FixedMove()
    {
        if (playerAnimator.GetBool("isRunning"))
        {
            //FlipSprite();
            playerRigidBody2D.MovePosition(playerRigidBody2D.position + velocity * Time.fixedDeltaTime);
            
        }

    }

    private void FlipSprite()
    {
        //are we running left?
        if (velocity.x < Mathf.Epsilon)
        {
            //rotate character so we appear to be running in opposite direction
            transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        else
        {
            //Reset rotation
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        
        
    }

}
