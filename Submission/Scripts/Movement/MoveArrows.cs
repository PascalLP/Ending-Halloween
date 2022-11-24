using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArrows : MonoBehaviour
{

    [SerializeField]
    float mMoveSpeed;
    [SerializeField]
    float mJumpForce;
    [SerializeField]
    LayerMask mWhatIsGround;
    float kGroundCheckRadius = 0.1f;

    // Animator booleans
    bool mRunning;
    bool mGrounded;
    bool mRising;
    //

    // Wall kicking
    //bool mAllowWallKick;
    Vector2 mFacingDirection;

    // Damage effects
    float kDamagePushForce = 2.5f;

    // Invincibility timer
    float kInvincibilityDuration = 1.0f;
    float mInvincibleTimer;
    bool mInvincible;
    bool mAimMode;

    // References to other components and game objects
    Animator mAnimator;
    Rigidbody mRigidBody;
    List<GroundCheck> mGroundCheckList;

    // Audio sources
    AudioSource mLandingSound;
    AudioSource mWallKickSound;
    AudioSource mTakeDamageSound;

    [SerializeField]
    GameObject mDeathParticleEmitter;
    [SerializeField]
    private int life;

    // Start is called before the first frame update
    void Start()
    {
        mRunning = false;

        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody = GetComponent<Rigidbody>();

        // Obtain ground check components and store in list
        mGroundCheckList = new List<GroundCheck>();
        GroundCheck[] groundChecksArray = transform.GetComponentsInChildren<GroundCheck>();
        foreach (GroundCheck g in groundChecksArray)
        {
            mGroundCheckList.Add(g);
        }

        // Get audio references
        AudioSource[] audioSources = GetComponents<AudioSource>();
        mLandingSound = audioSources[0];
        mWallKickSound = audioSources[1];
        mTakeDamageSound = audioSources[2];

    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(MovementX * Speed * Time.deltaTime, MovementY * Speed * Time.deltaTime);
        
        mAimMode = false;

        bool grounded = CheckGrounded();
        if (!mGrounded && grounded)
        {
            mLandingSound.Play();
        }
        mGrounded = grounded;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mRigidBody.AddForce(Vector2.up * mJumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * mMoveSpeed * Time.deltaTime);
            FaceDirection(-Vector2.right);
            mRunning = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * mMoveSpeed * Time.deltaTime);
            FaceDirection(Vector2.right);
            mRunning = true;
        } 

        if (Input.GetKey(KeyCode.RightArrow))
        {
            mRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightShift))
        {
            mRunning = false;
            mMoveSpeed = 7;
        }

        if (mRunning)
        {
            mMoveSpeed = 10;
        }

            if (mGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            mRigidBody.AddForce(Vector2.up * mJumpForce, ForceMode.Impulse);
        }
        /*else if (*//*mAllowWallKick && *//*Input.GetButtonDown("Jump"))
        {
            mRigidBody.velocity = Vector2.zero;
            mRigidBody.AddForce(Vector2.up * mJumpForce, ForceMode.Impulse);
            //mWallKickSound.Play();
        }*/

        mRising = mRigidBody.velocity.y > 0.0f;
        UpdateAnimator();

        if (mInvincible)
        {
            mInvincibleTimer += Time.deltaTime;
            if (mInvincibleTimer >= kInvincibilityDuration)
            {
                mInvincible = false;
                mInvincibleTimer = 0.0f;
            }
        }
    }



    public void Die()
    {
        // TODO: Instantiate the particle effects "mDeathParticleEmitter"
        //       and destroy the "Mega Man" game object
    }

    public void TakeDamage(int dmg)
    {
        if (!mInvincible)
        {
            Vector2 forceDirection = new Vector2(-mFacingDirection.x, 1.0f) * kDamagePushForce;
            mRigidBody.velocity = Vector2.zero;
            mRigidBody.AddForce(forceDirection, ForceMode.Impulse);
            mInvincible = true;
            mTakeDamageSound.Play();
            //life.DeductHealth(dmg);
        }
    }

    public Vector2 GetFacingDirection()
    {
        return mFacingDirection;
    }

    private void FaceDirection(Vector2 direction)
    {
        mFacingDirection = direction;
        if (direction == Vector2.right)
        {
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
    }

    private bool CheckGrounded()
    {
        foreach (GroundCheck g in mGroundCheckList)
        {
            if (g.CheckGrounded(kGroundCheckRadius, mWhatIsGround, gameObject))
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isRunning", mRunning);
        mAnimator.SetBool("isGrounded", mGrounded);
        mAnimator.SetBool("isRising", mRising);
        mAnimator.SetBool("isHurt", mInvincible);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            ContactPoint[] contactPoints = col.contacts;
            foreach (ContactPoint p in contactPoints)
            {
                float angleDifference = Vector2.Angle(p.normal, Vector2.right);
                if (angleDifference < 5.0f || angleDifference > 175.0f)
                {
                    //mAllowWallKick = true;
                    return;
                }
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
           // mAllowWallKick = false;
        }
    }
}
