using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public Text resultText;
    int startIn5sec = 5;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.gameStart == false)
        {
            resultText.text = "Ready For Battle";
            InvokeRepeating("Count5sec", 3f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(resultText.text);
    }

    void Count5sec()
    {
        if(startIn5sec == -1)
        {
            CancelInvoke("Count5sec");
            GameManager.Instance.gameStart = true;
            resultText.text = "Fight!";
        }
        else
        {
            resultText.text = "Start Game In " + startIn5sec + "sec";
            startIn5sec--;
        }
    }
}
