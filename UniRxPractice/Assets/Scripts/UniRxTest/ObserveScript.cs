using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObserveScript : MonoBehaviour
{
    TestUniRX testObject;

    List<IDisposable> disposables;

    // Start is called before the first frame update
    void Start()
    {
        disposables = new List<IDisposable>();
        testObject = FindObjectOfType<TestUniRX>();

        testObject.isPopUp.Subscribe(isOn =>
        {
            Debug.Log($"Another Object {isOn}");
        });

        var subScriber = testObject.subLanguage.Subscribe(language =>
        {
            switch (language)
            {
                case Language.KOREAN:
                case Language.ENGLISH:
                case Language.JAPANESE:
                default:
                    Debug.Log($"Language is {language} Set Something");
                    break;
            }
        });

        var subScriber2 = testObject.subLanguage
            .Where(language => language == Language.KOREAN)
            .Subscribe(language => Debug.Log("나는 한국인입니다."));

        var subScriber3 = testObject.subLanguage
            .Where(language => language == Language.ENGLISH)
            .Subscribe(language => Debug.Log("I'm English"));

        disposables.Add(subScriber);
        disposables.Add(subScriber2);
        disposables.Add(subScriber3);
    }

    public void DisposeSubscribe()
    {
        disposables[0].Dispose();
    }

    public void DisposeSubscribe2()
    {
        disposables[1].Dispose();
    }

    public void DisposeSubscribe3()
    {
        disposables[2].Dispose();
    }
}
