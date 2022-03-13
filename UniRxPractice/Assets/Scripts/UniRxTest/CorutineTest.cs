using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CorutineTest : MonoBehaviour
{
    IDisposable cancelEvent;
    // Start is called before the first frame update
    void Start()
    {
        cancelEvent = Observable.FromCoroutine(AsyncA).
                          SelectMany(AsyncB).
                          Subscribe();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SpaceBar Dispose!");
            cancelEvent.Dispose();
        }
    }

    IEnumerator AsyncA()
    {
        Debug.Log("a start");
        yield return new WaitForSeconds(1);
        Debug.Log("a end");
    }

    IEnumerator AsyncB()
    {
        Debug.Log("b start");
        yield return new WaitForSeconds(3);
        Debug.Log("b end");
    }

    IEnumerator AsyncC()
    {
        Debug.Log("c start");
        yield return new WaitForSeconds(5);
        Debug.Log("c end");
    }
}
