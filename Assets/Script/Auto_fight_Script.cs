using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BotType 
{
knight,
undead
};

public class Auto_fight_Script : MonoBehaviour
{
    public GameObject FightTarget;
    public float moveSpeed = 1f;//움직이는 속도로 디버그용으로 우선 public 선언
    
    public BotType botType;
    CharacterController characterController;
    Animator characterAnimator;
    float atkPerSec;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();
        atkPerSec = Random.Range(2.0f, 6.0f);
        StartCoroutine(CharacterAttack());
        Debug.Log(gameObject.name + "'s atkPeriod is : " + atkPerSec);
    }

    // Update is called once per frame
    void Update()
    {
        CharacterAttack();
        if (GameManager.Instance.gameStart && !GameManager.Instance.gameEnded)
        {
            if (!InAtkRange())
            {
                CharacterMove();
            }
            else
            {
                characterAnimator.SetInteger("anim_Status", 2);
            }
        }
        else if (GameManager.Instance.gameEnded)
        {
            StopAllCoroutines();
            characterAnimator.SetInteger("anim_Status", 0);
        }
    }

    /// <summary>
    /// 항상 상대하는 적을 바라보고 움직입니다.
    /// </summary>
    void CharacterMove()
    {
        transform.LookAt(FightTarget.transform);
        characterAnimator.SetInteger("anim_Status", 1);
        characterController.Move(moveSpeed * Time.deltaTime * transform.forward);
        if (InAtkRange())
            moveSpeed = 0f;
        else
            moveSpeed = 1f;
    }
    /// <summary>
    /// atkPerSec 주기로 적을 공격합니다.
    /// </summary>
    IEnumerator CharacterAttack()
    {
        while (true)
        {
            if (InAtkRange())
            {
                if (botType == BotType.knight)
                {
                    GameManager.Instance.undeadHP -= 20f;
                }
                else if (botType == BotType.undead)
                {
                    GameManager.Instance.knightHP -= 20f;
                }
            }
            yield return new WaitForSeconds(atkPerSec);
        }
        

    }
    private bool InAtkRange()
    {
        if(Vector3.Distance(transform.position, FightTarget.transform.position) <= 1.2f)
            return true;
        else
            return false;
    }
}
