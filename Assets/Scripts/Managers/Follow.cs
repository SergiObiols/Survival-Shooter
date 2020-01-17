using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;
    public float MinModifier = 7;
    public float MaxModifier = 11;

    Vector3 _velocity = new Vector3(0, 0, 0);
    bool _isFollowing = false;

    public void StartFollowing()
    {
        _isFollowing = true;
    }

    void Update()
    {
        if (_isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }
    }
}
