using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkill 
{
    NormalAttack, NormalAttack2, NormalAttack3,
}
public class SkillManager : MonoBehaviour
{
    [SerializeField]
    [Header("普通攻擊")]
    public GameObject NormalAttack1;
    public GameObject NormalAttack2;
    public GameObject NormalAttack3;

    private PlayerManager playerManager;

    private int NormalAttackType = 0;
    private IEnumerator comboCoroutine;
    public bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPlayerSkill(PlayerSkill m_playerskill) 
    {
        if (isAttack){ return; }
        switch (m_playerskill) 
        {
            case PlayerSkill.NormalAttack:
                CheckPlayerNormalAttack();
            break;
        }
    }
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------
    private void CheckPlayerNormalAttack()
    {
        //判斷目前是普通攻擊幾段
        switch (NormalAttackType)
        {
            case 0:
                print("普通攻擊一");
                var skill_object1 = Instantiate(NormalAttack1, gameObject.transform);

                //進行連招，中止上一個Combo倒數
                float combotime1 = skill_object1.GetComponent<SkillController>().ComboTime;
                comboCoroutine = ComboTime(combotime1);
                StopAllCoroutines();
                StartCoroutine(comboCoroutine);
                NormalAttackType = 1;
            break;

            case 1:
                print("普通攻擊二");
                var skill_object2 = Instantiate(NormalAttack2, gameObject.transform);

                //進行連招，中止上一個Combo倒數
                float combotime2 = skill_object2.GetComponent<SkillController>().ComboTime;
                comboCoroutine = ComboTime(combotime2);
                StopAllCoroutines();
                StartCoroutine(ComboTime(combotime2));
                NormalAttackType = 2;
            break;

            case 2:
                print("普通攻擊三");
                var skill_object3 = Instantiate(NormalAttack3, gameObject.transform);
                float combotime3 = skill_object3.GetComponent<SkillController>().ComboTime;
                comboCoroutine = ComboTime(combotime3);
                //進行連招，中止上一個Combo倒數
                StopAllCoroutines();
                //float combotime3 = skill_object3.GetComponent<SkillController>().ComboTime;
                NormalAttackType = 0;
            break;
        }
    }
    IEnumerator ComboTime(float time)
    {
        //進入連招倒數，如果超過Combo時間沒有進行Combo，段數就會重置
        yield return new WaitForSeconds(time);
        NormalAttackType = 0;
        //if (isNormalAttackCombo)
        //{
        //    //超過連段時間就會重置
        //    yield return new WaitForSeconds(time);
        //    isNormalAttackCombo = false;
        //    NormalAttackType = 0;
        //}

    }
}
