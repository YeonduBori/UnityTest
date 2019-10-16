using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    // Start is called before the first frame update
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (_instance == null)
                {
                    Debug.Log("There is no AppManager");
                }
            }
            return _instance;
        }
    }
    [Header("시작,끝boolean")]
    public bool gameStart = false;
    public bool gameEnded = false;
    [Header("Bot HP")]
    public float knightHP, undeadHP = 100f;
    [Header("HPBar")]
    public Image knightBar, undeadBar;
    void Update()
    {
        HPBar_Visual();

    }

    /// <summary>
    /// knight 와 undead의 HP bar(Image)의 변화를 가시화해주는 함수입니다.
    /// </summary>
    void HPBar_Visual()
    {
        knightBar.fillAmount = knightHP / 100;
        undeadBar.fillAmount = undeadHP / 100;
    }
    /// <summary>
    /// knight와 undead 중 한쪽의 피가 다 닳으면 게임을 즉시 종료시키는 gameEnded를 true로 변환
    /// </summary>
    void HpZero()
    {
        if(knightHP <= 0 || undeadHP <= 0)
        {
            gameEnded = true;
        }
    }
}
