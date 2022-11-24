using System.Collections;
using UnityEngine;


public class Gun : MonoBehaviour
{

    /*public int gunDamage = 1;
    public float gunRange = 50f;
    public float hitForce = 100f;*/
    public Transform firePoint;
    public GameObject projectile;
    public float fireRate = .5f;
    public ArrowMove mPlayer;
    float nextBullet;

    // Audio
    private AudioSource gunAS;
    /*private LineRenderer laserLine;
    private float nextFire;*/

    

    void Awake()
    {
        gunAS = GetComponent<AudioSource>();
        nextBullet = 0f;
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            Shoot();
        }

    }

    void Update()
    {

        if(Input.GetAxisRaw("Fire1")>0 && nextBullet < Time.time)
        {
            nextBullet = Time.time + fireRate;
            Vector3 rotation;
            if (mPlayer.GetFacing() == -1f)
                rotation = new Vector3(0, -90, 0);
            else
                rotation = new Vector3(0, 90, 0);

            Instantiate(projectile, transform.position, Quaternion.Euler(rotation));
        }
    }

    void Shoot()
    {
        gunAS.Play();
        Instantiate(projectile, firePoint.position, firePoint.rotation);
        Destroy(projectile);
    }
}
