using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBarChange : MonoBehaviour
{
    public Image Bar;
    float hp = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            GameManager.Instance.gameEnded = true;
        }
        else
        {
            hp -= Time.deltaTime * 100;
            
        }
        Bar.fillAmount = hp / 100;
    }
}
