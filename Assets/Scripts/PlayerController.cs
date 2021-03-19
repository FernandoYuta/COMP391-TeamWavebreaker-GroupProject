using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask platLayer;

    public float playerSpeed = 1f;
    public float jumpHeight = 1f;

    Rigidbody2D rbody;
    BoxCollider2D boxCollider2d;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space) && IsPlayerOnGround())
        {
            rbody.velocity = Vector2.up * jumpHeight;         
        }
        
        
        float speedX = Input.GetAxis("Horizontal");

        
        rbody.velocity = new Vector2(speedX*playerSpeed, rbody.velocity.y);
          
    }

    private bool IsPlayerOnGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platLayer);

        //Debug.Log(raycastHit2D.collider);

       return raycastHit2D.collider != null;        
    }

    
    
}
