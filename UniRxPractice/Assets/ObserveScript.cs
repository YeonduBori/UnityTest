using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObserveScript : MonoBehaviour
{
    TestUniRX testObject;

    // Start is called before the first frame update
    void Start()
    {
        testObject = FindObjectOfType<TestUniRX>();

        testObject.isPopUp.Subscribe(isOn =>
        {
            Debug.Log($"Another Object {isOn}");
        });

        testObject.subLanguage.Subscribe(language =>
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
    }
}
