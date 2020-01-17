using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveLife : MonoBehaviour
{
    public PlayerHealth playerHeal;
    public ParticleSystem systemParticlesHeart;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHeal.Heal();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerHeal.currentHealth < playerHeal.startingHealth)
            {
                systemParticlesHeart.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            systemParticlesHeart.Stop();
        }
    }
}
