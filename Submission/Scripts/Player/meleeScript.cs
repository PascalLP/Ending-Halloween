using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeScript : MonoBehaviour
{

    public float damage;
    public float knockback;
    public float knockBackRadius;
    public float meleeRate;

    private float nextMelee;

    private int shootableMask;

    GameObject player;
    Animator mAnim;
    ArrowMove mPlayerC;
    playerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootableMask = LayerMask.GetMask("Shootable");
        mAnim = player.GetComponentInParent<Animator>();
        mPlayerC = player.GetComponent<ArrowMove>();
        nextMelee = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //bool melee = Input.GetKeyDown(KeyCode.M);

        if(Input.GetKey(KeyCode.V) && nextMelee < Time.time && !(mPlayerC.GetRunning()))
        {
            // Animation
            mAnim.SetTrigger("gunMelee");
            nextMelee = Time.time + meleeRate;

            // Do damage
            Collider[] attacked = Physics.OverlapSphere(transform.position, knockBackRadius, shootableMask);


        }
    }
}
