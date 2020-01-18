using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float timeBetweenProjectile = 10f;
    public float range = 100f;
    public GameObject projectile;
    
    Text ammoText;

    float bulletTimer;
    float projectileTimer = 10f;
    Ray shootRay;
    RaycastHit shootHit;
    int remainingShots;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        ammoText = GameObject.FindGameObjectWithTag("AmmoTextTag").GetComponent<Text>();
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        remainingShots = 500;
        ammoText.text = remainingShots.ToString();
    }


    void Update ()
    {
        bulletTimer += Time.deltaTime;
        projectileTimer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && bulletTimer >= timeBetweenBullets && Time.timeScale != 0 && remainingShots>=1)
        {
            Shoot ();
        }

        if(Input.GetButton("Fire2") && projectileTimer >= timeBetweenProjectile && Time.timeScale != 0)
        {
            projectileTimer = 0f;

            GameObject explosive = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            explosive.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
        }

        if(bulletTimer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        bulletTimer = 0f;

        remainingShots = int.Parse(ammoText.text);
        remainingShots -= 1;
        ammoText.text = remainingShots.ToString();

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
