using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : MonoBehaviour
{
    public ParticleSystem explosionParticles;
    public Rigidbody playerRB;

    AudioSource explosionAudio;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Fiiiiuuu!!");
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    { 

        if(collision.rigidbody != playerRB)
        {
            Destroy(gameObject);

            ParticleSystem boom = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            boom.Play();

            Debug.Log("BOOM!");

            //explosionAudio.Play();
        }
    }
}
