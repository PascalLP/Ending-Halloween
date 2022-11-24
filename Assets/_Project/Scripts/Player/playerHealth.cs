using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerHealth : MonoBehaviour
{
    public static bool isDead = false;
    public float fullHealth;
    float currentHealth;

    public GameObject deathFX;

    // HUD
    public Slider playerHealthSlider;
    public Image damageIndicator;
    Color flashColor = new Color(255f, 255f, 255f, 0.3f);
    private float flashSpeed = 5f;
    private bool damaged = false;

    // Audio
    AudioSource playerAS;
    public AudioClip playerDeathSound;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;

        //HUF Initialization
        playerHealthSlider.maxValue = fullHealth;
        playerHealthSlider.value = currentHealth;

        playerAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we take damage to flash the screen
        if (damaged)
        {
            damageIndicator.color = flashColor;
        } else
        {
            damageIndicator.color = Color.Lerp(damageIndicator.color, Color.clear, flashSpeed*Time.deltaTime);
        }
        if(currentHealth>20)
            damaged = false;
        else
        {
            flashColor = new Color(255f, 255f, 255f, 0.05f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;
        playerHealthSlider.value = currentHealth;
        damaged = true;

        playerAS.Play();

        if (currentHealth <= 0)
        {
            damageIndicator.color = Color.Lerp(damageIndicator.color, new Color(255f, 255f, 255f, 0.4f), flashSpeed * Time.deltaTime);
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Dying");
        GameObject particle = Instantiate(deathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        Destroy(particle, 1f);
        

        if (!isDead)
        {
            Destroy(gameObject);
            isDead = true;
        }
    }
}
