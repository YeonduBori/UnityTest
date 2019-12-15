using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleCol : MonoBehaviour
{

    void Start()
    {
    }

    void OnParticleCollision(GameObject other)
    {
        if(other == null)
        {
            Debug.Log("nothing to effected");
        }
        Debug.Log(other);
    }
}
