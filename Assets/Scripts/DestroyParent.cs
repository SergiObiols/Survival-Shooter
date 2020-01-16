using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyParent : MonoBehaviour
{
    Slider soulSlider;

    void Start()
    {
        soulSlider = GameObject.FindGameObjectWithTag("soulSliderTag").GetComponent<Slider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(soulSlider.value < 200) soulSlider.value += 1;
            Destroy(transform.parent.gameObject);

        }
    }
}
