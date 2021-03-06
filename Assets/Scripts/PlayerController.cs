using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask platLayer;
    [SerializeField] GameObject slashVFX;
    [SerializeField] GameObject slashVFXLeft;
    [SerializeField] GameObject SlashSpawnerRight;
    [SerializeField] GameObject SlashSpawnerLeft;

    public float playerSpeed = 2.5f;
    public float jumpHeight = 5.5f;
    public float jumpPushForce = -1f;
    public float jumpVertForce = 20f;
    bool facingRight = true;

    //public float slideSpeed = 0.1f;


    Rigidbody2D rbody;
    BoxCollider2D boxCollider2d;
    Transform playerPos;
    PlayerAnimation playerAnimation;
 

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        playerPos = GetComponent<Transform>();
        playerAnimation = GetComponent<PlayerAnimation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float speedX = Input.GetAxis("Horizontal");

        if ((speedX > 0 && !facingRight) || (speedX < 0 && facingRight))
        {
            Flip();
        }

        if(speedX == 0)
        {
            playerAnimation.PlayerIdle(true);
        }

        if (speedX != 0 && rbody.velocity.y == 0)
        {
            playerAnimation.PlayerRun(true);
        }

        if (Input.GetAxis("Fire1") != 0)
        {
            playerAnimation.AttackAnim(true);

        }

        if (IsPlayerOnRightWall()|| IsPlayerOnLeftWall())
        {
            rbody.gravityScale = 0;
            rbody.velocity = new Vector2(speedX * playerSpeed, 0);

        }
        else
        {
            
            rbody.velocity = new Vector2(speedX * playerSpeed, rbody.velocity.y);
            rbody.gravityScale = 1;

        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rbody.velocity = new Vector2(speedX * playerSpeed, rbody.velocity.y);
            //Debug.Log("Space was pressed");

            if (IsPlayerOnGround())
            {
                rbody.velocity = Vector2.up * jumpHeight;
                playerAnimation.PlayerJump(true);
            }
            if (IsPlayerOnRightWall())
            {
                //Debug.Log("Space was pressed on right wall");
                //rbody.gravityScale = 1;
                //rbody.position.Set(rbody.position.x - 2f, rbody.position.y);
                rbody.position = new Vector2(rbody.position.x - 0.2f, rbody.position.y);
                //rbody.AddForce(new Vector2(jumpPushForce, jumpVertForce));
                rbody.velocity = new Vector2(jumpPushForce, jumpVertForce);
            }

        }

        //Debug.Log(rbody.velocity);



    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private bool IsPlayerOnGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platLayer);

        //Debug.Log(raycastHit2D.collider);

       return raycastHit2D.collider != null;        
    }

    private bool IsPlayerOnRightWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.right, 0.09f, platLayer);

        //Debug.Log(raycastHit2D.collider);

        return raycastHit2D.collider != null;
    }

    private bool IsPlayerOnLeftWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.left, 0.09f, platLayer);
        return raycastHit2D.collider != null;
    }


    public void SpawnSlash()
    {
        if (facingRight)
        {
            GameObject obj = GameObject.Instantiate(slashVFX, SlashSpawnerRight.transform.position, SlashSpawnerRight.transform.rotation);
        }
        else if(!facingRight)
        {
            GameObject obj = GameObject.Instantiate(slashVFXLeft, SlashSpawnerRight.transform.position, slashVFXLeft.transform.rotation);
        }
    }
}
