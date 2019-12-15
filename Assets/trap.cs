using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    public GameObject child;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject + "col enter");
        if(collision.gameObject.CompareTag("Bot"))
        {
            child.SetActive(true);
        }
    }
}
