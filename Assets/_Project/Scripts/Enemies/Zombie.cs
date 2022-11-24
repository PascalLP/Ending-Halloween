using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int health = 100;
    public Transform target;
    public float speed = 3f;
    Rigidbody rb;


    //public GameObject deathEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        // Makes enemy move towards player
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(pos);
        transform.LookAt(target);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        int points = (int)Random.Range(1f, 3f);
        Score.instance.AddPoints(points);
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
