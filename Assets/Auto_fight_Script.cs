using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_fight_Script : MonoBehaviour
{
    public GameObject FightTarget;
    public float moveSpeed = 3f;//움직이는 속도로 디버그용으로 우선 public 선언
    CharacterController characterController;
    Animator characterAnimator;
    private bool atkInRange = false;
    enum BotType {knight, undead};
    BotType botType;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();
        StartCoroutine(CharacterAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStart && !GameManager.Instance.gameEnded)
        {
            if (!atkInRange)
            {
                CharacterMove();
            }
            else
            {
                CharacterAttack();
            }
        }
        else if (GameManager.Instance.gameEnded)
        {
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
    }
    /// <summary>
    /// 적을 공격합니다.
    /// </summary>
    IEnumerator CharacterAttack()
    {
        characterAnimator.SetInteger("anim_Status", 2);
        if(botType == BotType.knight)
        {
            GameManager.Instance.undeadHP -= 20f;
        }
        else if(botType == BotType.undead)
        {
            GameManager.Instance.knightHP -= 20f;
        }
        characterAnimator.SetInteger("anim_Status", 0);
        yield return new WaitForSeconds(1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform == FightTarget.transform)
        {
            atkInRange = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform == FightTarget.transform)
        {
            atkInRange = false;
        }
    }
}
