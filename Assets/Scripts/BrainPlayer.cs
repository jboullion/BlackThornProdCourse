using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BrainPlayer : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] int health = 5;
    [SerializeField] Transform leftEye;
    [SerializeField] Transform rightEye;
    [SerializeField] Transform leftPupil;
    [SerializeField] Transform rightPupil;
    float eyeClamp = 0.06f;


    // State
    bool isAlive = true;
    //bool turning = false;
    Vector2 velocity;
    Vector2 mouseDirection;

    // GameObjects
    Rigidbody2D playerRigidBody2D;
    Animator playerAnimator;
    BoxCollider2D playerFeetCollider2D;


    void Awake()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
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
        MoveEyes();
    }
        
    // Move the pupils inside of the eyes while moving the controller
    private void MoveEyes()
    {

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
           
        }

        // Directional Inputs
        float h_movement = CrossPlatformInputManager.GetAxis("Horizontal");
        float v_movement = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(h_movement, v_movement);

        // Mouse Input
        mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;// - transform.position;

        // Normalize so diagonal movements do not increase speed
        velocity = moveInput.normalized * runSpeed;

        // If moving start running, if not moving stop movement and animations
        if(moveInput != Vector2.zero)
        {
            playerAnimator.SetBool("isRunning", true);
        }


        // Update Eyeball Position
        leftPupil.position = Vector2.MoveTowards(leftPupil.position, mouseDirection, 1.5f * Time.deltaTime);
        rightPupil.position = Vector2.MoveTowards(rightPupil.position, mouseDirection, 1.5f * Time.deltaTime);

        // We have to keep our pupils inside our eye balls
        float lClampedPositionX = Mathf.Clamp(leftPupil.position.x, leftEye.position.x - eyeClamp, leftEye.position.x + eyeClamp);
        float lClampedPositionY = Mathf.Clamp(leftPupil.position.y, leftEye.position.y - eyeClamp, leftEye.position.y + eyeClamp);
        float rClampedPositionX = Mathf.Clamp(rightPupil.position.x, rightEye.position.x - eyeClamp, rightEye.position.x + eyeClamp);
        float rClampedPositionY = Mathf.Clamp(rightPupil.position.y, rightEye.position.y - eyeClamp, rightEye.position.y + eyeClamp);

        // CLAMP our pupil position
        leftPupil.position = Vector2.Lerp(leftPupil.position, new Vector2(lClampedPositionX, lClampedPositionY), 1f);
        rightPupil.position = Vector2.Lerp(rightPupil.position, new Vector2(rClampedPositionX, rClampedPositionY), 1f);

    }

    // Move the Player
    private void FixedMove()
    {
        // Update Current Animation
        if (playerAnimator.GetBool("isRunning"))
        {
            playerRigidBody2D.MovePosition(playerRigidBody2D.position + velocity * Time.fixedDeltaTime);
        }

        // Rotate Player to face mouse
        //float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //transform.rotation = rotation;

    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
