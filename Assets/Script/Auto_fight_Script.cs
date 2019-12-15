using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_fight_Script : MonoBehaviour
{
    public GameObject FightTarget;
    public float moveSpeed = 1f;//움직이는 속도로 디버그용으로 우선 public 선언
    public GameObject[] curStatus;
    public enum BotType {knight, undead};
    public BotType botType;
    CharacterController characterController;
    Animator characterAnimator;
    float atkPerSec;
    float tempAtkSpd;
    enum status { normal, iced};
    status ObjStatus = status.normal;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();
        atkPerSec = Random.Range(4.0f, 6.0f);
        tempAtkSpd = atkPerSec;
        StartCoroutine(CharacterAttack());
    }

    // Update is called once per frame
    void Update()
    {
        CharacterAttack();
        if (GameManager.Instance.gameStart && !GameManager.Instance.gameEnded)
        {
            if (!InAtkRange() && ObjStatus == status.normal)
            {
                CharacterMove();
            }
            else if(InAtkRange() && ObjStatus == status.normal)
            {
                characterAnimator.SetInteger("anim_Status", 2);
            }
            else if(ObjStatus == status.iced)
            {
                characterAnimator.SetInteger("anim_Status", 0);
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
        ObjStatus = status.normal;
        transform.LookAt(FightTarget.transform);
        characterAnimator.SetInteger("anim_Status", 1);
        characterController.Move(moveSpeed * Time.deltaTime * transform.forward);
        if (InAtkRange())
        {
            moveSpeed = 0f;
        }
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
                    GameManager.Instance.undeadHP -= 10f;
                }
                else if (botType == BotType.undead)
                {
                    GameManager.Instance.knightHP -= 10f;
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
    
    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other);
        if (other.CompareTag("fire"))
        {
            Invoke("FirePower", 1f);
            Debug.Log(gameObject + "On" + other.tag + " atk : " + atkPerSec + " movSpd : " + moveSpeed);
        }
        if (other.CompareTag("rain"))
        {
            RainPower();
            Debug.Log(gameObject + "On" + other.tag + " atk : " + atkPerSec + " movSpd : " + moveSpeed);
        }
        if (other.CompareTag("ice"))
        {
            IcePower();
            Debug.Log(gameObject + "On" + other.tag + " atk : " + atkPerSec + " movSpd : " + moveSpeed);
        }
        if (other.CompareTag("snow"))
        {
            SnowPower();
            Debug.Log(gameObject + "On" + other.tag + " atk : " + atkPerSec + " movSpd : " + moveSpeed);
        }
    }

    //비에 맞고 있으면 공격 속도가 저하된다.
    void RainPower()
    {
        curStatus[2].SetActive(true);
        if (botType == BotType.undead)
        {
            atkPerSec = 20f;
        }
        
    }

    //불에 닿으면 데미지를 입는다.
    void FirePower()
    {
        curStatus[0].SetActive(true);
        if (botType == BotType.undead)
        {
            GameManager.Instance.undeadHP -= 0.075f;
        }
        else if(botType == BotType.knight)
        {
            GameManager.Instance.knightHP -= 0.05f;
        }
    }

    //얼음에 닿으면 3초간 동작을 멈추고 데미지를 입는다.
    void IcePower()
    {
        curStatus[1].SetActive(true);
        if (botType == BotType.undead)
        {
            moveSpeed = 0f;
            ObjStatus = status.iced;
            GameManager.Instance.undeadHP -= 10f;
            Invoke("RestorMoveSpeed", 3f);
        }
    }

    //눈이 내리고 있으면 이동속도가 50프로 감소한다.
    void SnowPower()
    {
        curStatus[3].SetActive(true);
        moveSpeed = 0.5f;
    }

    void RestorMoveSpeed()
    {
        moveSpeed = 1.0f;
        ObjStatus = status.normal;
    }
}
