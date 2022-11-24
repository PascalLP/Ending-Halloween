using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    // Movement variables
    public float walkSpeed;
    public float runSpeed;
    private bool isRunning;

    Rigidbody rb;
    Animator mAnim;

    private bool facingRight;

    // for jumping
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mAnim = GetComponentInChildren<Animator>();
        walkSpeed = 4f;
        runSpeed = 8f;
        jumpHeight = 4f;
        facingRight = true;
       
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    void FixedUpdate()
    {
        isRunning = false;

        if (!playerHealth.isDead)
        {
            if(grounded && Input.GetAxis("Jump") > 0)
            {
                grounded = false;
                mAnim.SetBool("grounded", grounded);
                rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            }

            // Checking grounded
            groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
            if (groundCollisions.Length > 0) grounded = true;
            else grounded = false;

            mAnim.SetBool("grounded", grounded);

            // Sideways movement
            float move = Input.GetAxis("Horizontal");
            // Send value to animator
            mAnim.SetFloat("move", Mathf.Abs(move));

            // Running
            float running = Input.GetAxisRaw("Fire3");
            mAnim.SetFloat("running", running);

            float firing = Input.GetAxisRaw("Fire1");
            mAnim.SetFloat("shooting", firing);

            if ((running > 0) && grounded)
            {
                rb.velocity = new Vector3(move * runSpeed, rb.velocity.y, 0);
                if (Mathf.Abs(move) > 0)
                    isRunning = true;
                firing = 0f;
            }
            else
            {
                rb.velocity = new Vector3(move * walkSpeed, rb.velocity.y, 0);
                isRunning = false;
            }


            if (move > 0 && !facingRight) Flip();
            else if (move < 0 && facingRight) Flip();

        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.z *= -1;
        transform.localScale = scale;
    }

    public float GetFacing()
    {
        if (facingRight) return 1;
        else return -1;
    }

    public bool GetRunning()
    {
        return isRunning;
    }

}
