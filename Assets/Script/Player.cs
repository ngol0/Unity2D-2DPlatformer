using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //variables
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] GameObject SlashPrefab;
    [SerializeField] GameObject gun;
    //[SerializeField] GameObject deathVFX;

    Vector2 moveInput;
    float startingGravity;   
    bool isAlive = true;

    //property
    [field: SerializeField] public int Health { get; set; } = 100;

    // reference
    Rigidbody2D myrigidbody;
    Animator myanimation;
    CapsuleCollider2D myCollider;
    BoxCollider2D myFeetCollider;


    //bool check;

    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myanimation = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        startingGravity = myrigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) {  return; }
        else
        {
            Run();
            Climb();
            HitHazard();
        }
        
    }

    //Move Function
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();        
    }

    //Jump Function
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        bool touchGround = IsTouchingGround(myFeetCollider);

        //if not touch the ground - not jumping anymore
        if (!touchGround)
        {
            return;
        }

        //if touch the ground and value is pressed -> jump
        if (value.isPressed)
        {
            myrigidbody.velocity += new Vector2(0f, jumpSpeed);
        }

    }

    //Fire
    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }

        Coroutine FireCoroutine = StartCoroutine(Fire());

    }

    //Check to see if player is touching the ground
    private bool IsTouchingGround(BoxCollider2D player)
    {
        int ground = LayerMask.GetMask("Ground", "Box");

        return player.IsTouchingLayers(ground);
    }

    //Check to see if player is touching ladder
    private bool IsTouchingLadder(CapsuleCollider2D player) {
        int ladder = LayerMask.GetMask("Ladder");

        return player.IsTouchingLayers(ladder);
    }


    //touching hazard
    private bool IsTouchingHazardUp(CapsuleCollider2D player)
    {
        int hazard_Up = LayerMask.GetMask("Hazard");

        return player.IsTouchingLayers(hazard_Up);
    }

    private bool IsTouchingHazardDown(BoxCollider2D player)
    {
        int hazard_Down = LayerMask.GetMask("Hazard");

        return player.IsTouchingLayers(hazard_Down);
    }

    //touching enemy
    private bool IsTouchingEnemy(CapsuleCollider2D player)
    {
        int enemy = LayerMask.GetMask("Enemy");

        return player.IsTouchingLayers(enemy);
    }


    //Running Update
    private void Run()
    {

        //Set Movement
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myrigidbody.velocity.y);
        
        myrigidbody.velocity = playerVelocity;

        //if the player is running
        if (Mathf.Abs(playerVelocity.x) > Mathf.Epsilon)
        {
            //Flip Sprite
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);

            //Set Movement animation
            myanimation.SetBool("isRunning", true);
        }

        else
        {
            //Set idling animation
            myanimation.SetBool("isRunning", false);
        }

     }

    private void Climb()
    {
        bool touchLadder = IsTouchingLadder(myCollider);

        if (!touchLadder)
        {
            myrigidbody.gravityScale = startingGravity;

            //idling
            myanimation.SetBool("isClimbing", false);
            myanimation.SetBool("climbStill", false);

            return;
        }

        else
        {
            Vector2 playerClimbVelocity = new Vector2(myrigidbody.velocity.x, moveInput.y * climbSpeed);

            myrigidbody.velocity = playerClimbVelocity;

            //prevent sliding
            myrigidbody.gravityScale = 0f;

            //set up climbing movement
            myanimation.SetBool("climbStill", true);

            //Move when climbing and stand still when not moving up
            bool climbing = Mathf.Abs(playerClimbVelocity.y) > Mathf.Epsilon;
            if (climbing)
            {
                myanimation.SetBool("isClimbing", true);
                myanimation.SetBool("climbStill", false);
            }
            else
            {
                myanimation.SetBool("isClimbing", false);
                myanimation.SetBool("climbStill", true);
            }

        }

    }

    void HitHazard()
    {
        bool touchHazardUp = IsTouchingHazardUp(myCollider);
        bool touchHazardDown = IsTouchingHazardDown(myFeetCollider);
        //Debug.Log(touchWater);
        if (touchHazardUp || touchHazardDown)
        {
            Die();
        }

        bool touchEnemy = IsTouchingEnemy(myCollider);
        if (touchEnemy)
        {
            Die();

        }

    }


    public void Die()
    {
        
        isAlive = false;

        myrigidbody.velocity = new Vector2(0f, 15f);

        FindObjectOfType<GameSession>().
            processPlayerDeath();

    }

    //set slasing animation
    IEnumerator Fire()
    {
        myanimation.SetBool("isSlashing", true);

        yield return new WaitForSeconds(0.3f);

        myanimation.SetBool("isSlashing", false);
    }

    void Shoot()
    {
        var bullet = Instantiate(SlashPrefab, gun.transform.position,
Quaternion.identity);
    }

}
