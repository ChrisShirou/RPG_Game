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
        switch (NormalAttackType)
        {
            case 0:
                print("普通攻擊一");
                Instantiate(NormalAttack1, gameObject.transform);
                NormalAttackType = 1;
            break;

            case 1:
                print("普通攻擊二");
                Instantiate(NormalAttack2, gameObject.transform);
                NormalAttackType = 2;
            break;

            case 2:
                print("普通攻擊三");
                Instantiate(NormalAttack3, gameObject.transform);
                NormalAttackType = 0;
            break;
        }
    }
}
