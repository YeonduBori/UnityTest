using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class TestUniRX : MonoBehaviour
{
    public ReactiveProperty<bool> isPopUp = new ReactiveProperty<bool>(false);
    public ReactiveProperty<Language> subLanguage = new ReactiveProperty<Language>(Language.NONE);

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Set Obeservation Event");
        
        isPopUp.Subscribe(isOn =>
        {
            Debug.Log($"is {isOn} Do Something");
        });

        subLanguage.Subscribe(language =>
        {
            switch(language)
            {
                case Language.KOREAN:
                case Language.ENGLISH:
                case Language.JAPANESE:
                default:
                    Debug.Log($"Language : {language}");
                    break;
            }
        });

        
    }

    public void SetBool(bool isOn)
    {
        isPopUp.Value = isOn;
    }

    public void SetLanguage(int language)
    {
        subLanguage.Value = (Language)language;
    }    
}

public enum Language
{
    NONE,
    KOREAN,
    ENGLISH,
    JAPANESE,
    
    CountLanguage,
}
