using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTextControl : MonoBehaviour
{
    public Text resultText;
    int startIn5sec = 5;
    int fightTime = 60;
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
        if(GameManager.Instance.gameEnded)
        {
            string winner = "";
            //봇 양쪽 체력 비교
            if (GameManager.Instance.knightHP > GameManager.Instance.undeadHP)
                winner = "Knight";
            else
                winner = "Undead";
            resultText.text = "The Winner is " + winner + "!";
        }
    }

    void Count5sec()
    {
        if(startIn5sec > 0)
        {
            resultText.text = "Start Game In " + startIn5sec + "sec";
        }
        else if(startIn5sec == 0)
        {
            GameManager.Instance.gameStart = true;
            resultText.text = "Fight!";
            CancelInvoke("Count5sec");
            InvokeRepeating("Count60sec", 2f, 1f);
        }
        startIn5sec--;
        //Debug.Log(startIn5sec);//시간 구현 완료
    }

    void Count60sec()
    {
        if(fightTime > 0 && !GameManager.Instance.gameEnded)
        {
            resultText.text = "Time : " + fightTime;
            fightTime--;
        }
        else
        {
            CancelInvoke("Count60sec");
            GameManager.Instance.gameEnded = true;
        }
        //Debug.Log(fightTime);//시간 구현 완료
    }
}
