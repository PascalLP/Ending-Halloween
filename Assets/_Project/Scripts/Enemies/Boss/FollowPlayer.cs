using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    public float attackSpeed = 4;
    [SerializeField]
    public float attackDistance;
    [SerializeField]
    public float bufferDistance;
    [SerializeField]
    public GameObject player;

    //flipping model
    public bool facingRight = false;
    bool detected;

    // Aniamation
    Animator mAnimator;

    Transform playerTransform;

    void GetPlayerTransform()
    {
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.Log("Player not specified in Inspector");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        detected = false;
        GetPlayerTransform();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(playerTransform.position, transform.position);
        var flipPos = transform.position.x - playerTransform.position.x;
        mAnimator.SetFloat("distance", distance);

        // Facing player
        //var look = transform.LookAt(playerTransform.position, Vector3.up);
        var dir = playerTransform.transform.position - transform.position;
        gameObject.transform.forward = new Vector3(dir.x, 0f, dir.z);

        //transform.rotation.x = 0;

        // Debug.Log("Distance to Player" + distance);
        if (distance <= attackDistance)
        {
            /*if(flipPos < 0)
            {
                Flip();
            }*/
            detected = true;
            mAnimator.SetBool("isDetected", detected);
            if (distance >= bufferDistance)
            {
                transform.position += transform.forward * attackSpeed * Time.deltaTime;
            }
        }
        else
        {
            detected = false;
            mAnimator.SetBool("isDetected", detected);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = gameObject.transform.localScale;
        theScale.z *= -1;
        gameObject.transform.localScale = theScale;
    }
}
