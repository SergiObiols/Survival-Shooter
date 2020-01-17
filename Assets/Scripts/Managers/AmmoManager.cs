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
    AudioSource reloadAudio;

  
    void OnTriggerEnter(Collider colider)
    {
        ammoText = GameObject.FindGameObjectWithTag("AmmoTextTag").GetComponent<Text>();
        reloadAudio = GetComponent<AudioSource>();

        if (colider.CompareTag("Player"))
        {
            currentAmount = int.Parse(ammoText.text)+ammoAmount;
            ammoText.text = currentAmount.ToString();
            reloadAudio.Play();
            particleSystemReload.Play();
        }

    }
}
