using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    public float damage;
    public float damageRate;
    public float pushBackForce;

    float nextDamage;

    bool playerInRange = false;

    GameObject player;
    playerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        nextDamage = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<playerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealth.isDead)
        {
            if (playerInRange) Attack();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    void Attack()
    {

        if (nextDamage <= Time.time)
        {
            Debug.Log("ATTACKING");
            playerHealth.TakeDamage(damage);
            nextDamage = Time.time + damageRate;

            pushBack(player.transform);
        }
    }

    void pushBack(Transform pushedObj)
    {
        Vector3 pushDirection = new Vector3((pushedObj.position.x - transform.position.x), (pushedObj.position.y - transform.position.y), 0).normalized;
        pushDirection *= pushBackForce;

        Rigidbody pushedRB = pushedObj.GetComponent<Rigidbody>();
        pushedRB.velocity = Vector3.zero;
        pushedRB.AddForce(pushDirection, ForceMode.Impulse);
    }
}

