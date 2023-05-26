using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public Animator _animator;
     void Update()
    {
        if (Input.anyKeyDown)
        {
            _animator.SetTrigger("Main");
            Destroy(gameObject);
        }
    }
}
