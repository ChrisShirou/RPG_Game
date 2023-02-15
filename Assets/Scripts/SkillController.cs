using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/*
[Header("詠唱時間")]
public float CastTime;
[Header("施放時間")]
public float SkillTime;
[Header("連招時間")]
public float ComboTime;
[Header("冷卻時間")]
public float ColdDownTime;
[Header("攻擊倍率")]
public float AttackMultiplier;
*/
public class SkillController : SkillStatus
{
    private GameObject Player;
    private PlayerManager playerManager;
    private SkillManager skillManager;
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
        playerManager = Player.GetComponent<PlayerManager>();
        skillManager = Player.GetComponent<SkillManager>();

        //切換狀態，攻擊中
        skillManager.isAttack = true;
        //招式施放結束才會消失
        StartCoroutine(AttackTime(SkillTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            playerManager.PlayerAttack(other.transform, AttackMultiplier);
        }
    }
    IEnumerator AttackTime(float time)
    {
        yield return new WaitForSeconds(time);
        skillManager.isAttack = false;
        Destroy(gameObject);
    }
}
