using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : MonoBehaviour
{
    public ParticleSystem explosionParticles;
    public float blastRadius = 5f;

    AudioSource explosionAudio;
    Ray knockRay;
    RaycastHit knockHit;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Fiiiiuuu!!");
    }

    private void Awake()
    {
        explosionAudio = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "Player")
        {
            Destroy(gameObject);

            ParticleSystem boom = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            boom.Play();

            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider nearObj in colliders)
            {
                EnemyHealth enemyHP = nearObj.GetComponent<EnemyHealth>();
                if (enemyHP != null)
                {
                    knockRay.origin = transform.position;
                    knockRay.direction = enemyHP.transform.position - transform.position;
                    Physics.Raycast(knockRay, out knockHit);

                    enemyHP.TakeDamage(80, knockHit);
                }
            }

            //explosionAudio.Play();
        }
    }
}
