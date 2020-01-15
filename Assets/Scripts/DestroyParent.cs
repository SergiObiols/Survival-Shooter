using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyParent : MonoBehaviour
{

	public Slider soulSlider;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(soulSlider.value < 100) soulSlider.value += 5;
            Destroy(transform.parent.gameObject);

        }
    }
}
