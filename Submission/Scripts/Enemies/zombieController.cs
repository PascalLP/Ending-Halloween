using UnityEngine;
using System.Collections;

public class zombieController : MonoBehaviour
{
    public GameObject flipModel;

    // Audio stuff
    public AudioClip[] idleSounds;
    public float idleSoundDelay;
    AudioSource enemyMoveAS;
    float nextIdleSound = 0f;

    public float detectionTime;
    float startRun;
    bool firstDetection;

    // Movement variables
    public float runSpeed;
    public float walkSpeed;
    public bool facingRight = true;

    float moveSpeed;
    bool running;

    Rigidbody rb;
    Animator mAnim;
    Transform detectedPlayer;

    bool detected;


    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        mAnim = GetComponentInParent<Animator>();
        enemyMoveAS = GetComponent<AudioSource>();

        running = false;
        detected = false;
        firstDetection = false;
        moveSpeed = walkSpeed;

        if (Random.Range(0, 10) > 5) Flip();

    }


    void FixedUpdate()
    {
        if (detected)
        {
            if (detectedPlayer.position.x < transform.position.x && facingRight)
            {
                Flip();
            } else if (detectedPlayer.position.x > transform.position.x && !facingRight){
                Flip();
            }

            if (!firstDetection)
            {
                startRun = Time.time + detectionTime;
                firstDetection = true;
            }
        }

        if(detected && !facingRight)
        {
            rb.velocity = new Vector3((moveSpeed * -1), rb.velocity.y, 0);
        } 
        if (detected && facingRight)
        {
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0);
        }

        if(!running && detected)
        {
            if (startRun < Time.time)
            {
                moveSpeed = runSpeed;
                mAnim.SetTrigger("run");
                running = true;
            }
        }

        // Sounds
        if (!running)
        {
            if(Random.Range(0,10)>5 && (nextIdleSound < Time.time)){
                AudioClip tempClip = idleSounds[Random.Range(0, idleSounds.Length)];
                enemyMoveAS.clip = tempClip;
                enemyMoveAS.Play();
                nextIdleSound = idleSoundDelay + Time.time;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !detected)
        {
            //firstDetection = false;

            detected = true;
            detectedPlayer = other.transform;
            mAnim.SetBool("detected", detected);
            if (detectedPlayer.position.x < transform.position.x && facingRight) Flip();
            else if (detectedPlayer.position.x > transform.position.x && !facingRight) Flip();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            firstDetection = false;
            if (running)
            {
                mAnim.SetTrigger("run");
                moveSpeed = walkSpeed;
                running = false;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1;
        flipModel.transform.localScale = theScale;
    }
}
