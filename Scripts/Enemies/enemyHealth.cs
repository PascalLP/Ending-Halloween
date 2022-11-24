using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public static bool isDead = false;
    public int enemyMaxHealth;
    public int damageModifier;
    //public GameObject damageParticles;

    private int currentHealth;

    // Audio
    AudioSource enemyAS;
    public AudioClip hurtSound;
    public AudioClip deathSound;

    GameObject player;
    Player playerPoints;


    void Awake()
    {
        currentHealth = enemyMaxHealth;
        enemyAS = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPoints = player.GetComponent<Player>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        enemyAS.clip = hurtSound;
        Debug.Log("You hit me");
        //damage *= damageModifier;
        if (damage <= 0)
        {
            Debug.Log("No damage done");
            return;
        }
        currentHealth -= damage;
        Debug.Log("Lowered health");
        if (!isDead) enemyAS.Play();

        if (currentHealth <= 0) Die();
    }

    /*public void DamageFX(Vector3 point, Vector3 rotation)
    {
        Instantiate(damageParticles, point, Quaternion.Euler(rotation));
    }*/

    void Die()
    {
        enemyAS.clip = deathSound;

        Debug.Log("Died");
        zombieController aZombie = GetComponentInChildren<zombieController>();
        if(aZombie != null)
        {
            
        }

        // Points given
        int points = Random.Range(1, 3);

        if (!isDead)
        {
            enemyAS.Play();
            playerPoints.scoreManager.AddPoints(points);
            Destroy(gameObject);
            
        }
    }
}
