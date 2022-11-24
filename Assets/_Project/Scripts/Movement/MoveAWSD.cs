using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAWSD : MonoBehaviour
{

    public float Speed = 5f;
    public float Acceleration = 10f;
    public float JumpSpeed = 7f;
    public float JumpDuration = 20f;

    private float MovementX;
    private float MovementY;
    private float JumpInput;

    // Internal variables
    private bool onGround;
    private float jumpDur;
    private bool jumpKeyDown = false;
    private bool canJump = false;

    Rigidbody rb;
    LayerMask layerMask;
    Transform modelTrans;

    public Vector3 lookPos;
    GameObject rsp;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MovementX = 0;
        MovementY = 0;

        layerMask = ~(1 << 8);

        rsp = new GameObject();
        rsp.name = transform.root.name + "Right Shoulder IK Helper";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputHandler();
        UpdateRigidbodyValues();
        MovementHandler();
        HandleRotation();
        HandleAimingPos();
    }

    void InputHandler()
    {
        MovementX = Input.GetAxis("Horizontal");
        MovementY = Input.GetAxis("Vertical");
        jumpKeyDown = Input.GetKeyDown(KeyCode.Space);
    }

    void MovementHandler()
    {
        onGround = isOnGround();

        if(MovementX < -0.1f)
        {
            if(rb.velocity.x > -this.Speed)
            {
                rb.AddForce(new Vector3(-this.Acceleration, 0, 0));
            }
            else
            {
                rb.velocity = new Vector3(-this.Speed, rb.velocity.y, 0);
            }
        }else if(MovementX > 0.1f)
        {
            if(rb.velocity.x < this.Speed)
            {
                rb.AddForce(new Vector3(this.Acceleration, 0, 0));
            }
            else
            {
                rb.velocity = new Vector3(this.Speed, rb.velocity.y, 0);
            }
        }

        if(jumpKeyDown)
        {
           /* if (!jumpKeyDown)
            {
                jumpKeyDown = true;*/

                if (onGround)
                {
                    rb.velocity = new Vector3(rb.velocity.y, this.JumpSpeed, 0);
                    JumpDuration = 0.0f;
                
            }else if (canJump)
            {
                JumpDuration += Time.deltaTime;

                if(JumpDuration < this.JumpDuration / 1000)
                {
                    rb.velocity = new Vector3(rb.velocity.x, this.JumpSpeed, 0);
                }
            }
        }
        else
        {
            jumpKeyDown = false;
        }

    }

    void HandleRotation()
    {
        Vector3 directionToLook = lookPos - transform.position;
        directionToLook.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);
    }

    void HandleAimingPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookPos = lookP;
        }
    }

    void UpdateRigidbodyValues()
    {
        if (onGround)
        {
            rb.drag = 4;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private bool isOnGround()
    {
        bool retVal = false;
        float lengthToSearch = 1.5f;

        Vector3 lineStart = transform.position + Vector3.up;

        Vector3 vectorToSearch = -Vector3.up;

        RaycastHit hit;

        if (Physics.Raycast(lineStart, vectorToSearch, out hit, lengthToSearch, layerMask))
        {
            retVal = true;
        }

        return retVal;
    }
}
