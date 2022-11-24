using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 50;
    public float speed = 20f;
    public float range = 10f;
    public float aliveTime = 5f;

    Rigidbody rb;
    Ray shootRay;
    RaycastHit shootHit;
    LayerMask shootableMask;

    private AudioSource bulletAS;


    // Start is called before the first frame update
    void Awake()
    {
        bulletAS = GetComponent<AudioSource>();
        bulletAS.Play();

        shootableMask = LayerMask.GetMask("Shootable");

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // hit an enemy
            // here
           
        }

        // Destroy bullet
        Destroy(gameObject, aliveTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        if(hitInfo.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit a zombie");
            hitInfo.gameObject.GetComponentInParent<enemyHealth>().TakeDamage(50);
            Destroy(gameObject);
        }

        if (hitInfo.gameObject.layer.ToString() == "Shootable")
        {
            Destroy(gameObject);
        }
    }
}
