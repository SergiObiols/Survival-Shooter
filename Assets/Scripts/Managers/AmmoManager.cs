using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{

    public int ammoAmount;
    private int currentAmount;
    Text ammoText;
    public ParticleSystem particleSystemReload;
    AudioSource playerAudio;
    public AudioClip reloadClip;

  
    void OnTriggerEnter(Collider colider)
    {
        ammoText = GameObject.FindGameObjectWithTag("AmmoTextTag").GetComponent<Text>();
        playerAudio = GetComponent<AudioSource>();

        if (colider.CompareTag("Player"))
        {
            currentAmount = int.Parse(ammoText.text)+ammoAmount;
            ammoText.text = currentAmount.ToString();
           // Destroy(transform.gameObject);
        }

    }

    void OnTriggerStart(Collider colider)
    {
        if (colider.CompareTag("Player"))
        {
            playerAudio.clip = reloadClip;
            playerAudio.Play();
            particleSystemReload.Play();
        }
    }
}
